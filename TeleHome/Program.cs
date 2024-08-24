using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using TeleHome.Models;
using Microsoft.AspNetCore.ResponseCompression;
using FluentValidation.AspNetCore;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddResponseCaching();


builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/html" });
});
builder.Services.AddMemoryCache();
 builder.Services.AddDbContext<RmlubecoTelehomeContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<RmlubecoTelehomeContext>();
builder.Services.AddAuthentication("UserAuthentication")
        .AddCookie("UserAuthentication", options =>
        {
            options.Cookie.Name = "User";
            options.LoginPath = "/Main/Login";
            options.AccessDeniedPath = "/Main/Login";
            options.ExpireTimeSpan = TimeSpan.FromHours(300);
            options.SlidingExpiration = false;
        })
        .AddCookie("BasketAuthentication", options =>
        {
            options.Cookie.Name = "Basket";
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
            options.AccessDeniedPath = "/Main/Login";
            options.LoginPath = "/Main/Login";
        });


builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(
    opt =>
    {
        var supportedCultures = new List<CultureInfo>
        {
                        new CultureInfo("az"),
                        new CultureInfo("ru")
        };
        opt.DefaultRequestCulture = new RequestCulture("az");
        opt.SupportedCultures = supportedCultures;
        opt.SupportedUICultures = supportedCultures;
    });

var smtpClient = new SmtpClient
{
    Host = "plesk3005.my-hosting-panel.com",
    Port = 587,
    UseDefaultCredentials = false,
    Credentials = new NetworkCredential("info@telehome.az", "TeleHome@123"),
    EnableSsl = true
};

builder.Services.AddSingleton(smtpClient);


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseResponseCaching();
app.UseResponseCompression();
app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 365;
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
    }
});
app.UseRouting();
app.UseDeveloperExceptionPage();
app.UseAuthentication();
app.UseAuthorization();


app.UseRequestLocalization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Home}/{id?}");

app.Run();
