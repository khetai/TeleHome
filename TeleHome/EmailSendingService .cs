using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Text;
using TeleHome.Models;

namespace TeleHome
{
    //public class EmailSendingService : BackgroundService
    //{
    //    private readonly RmlubecoTelehomeContext _db;
    //    private readonly SmtpClient _smtpClient;

    //    public EmailSendingService(RmlubecoTelehomeContext db, SmtpClient smtpClient)
    //    {
    //        _db = db;
    //        _smtpClient = smtpClient;
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
    //        }
    //    }

    //    public async Task SendEmails(Campaign campaign)
    //    {
    //        var subscribers = await _db.Subscribes.ToListAsync();

    //        foreach (var subscriber in subscribers)
    //        {
    //            var mailMessage = new MailMessage
    //            {
    //                From = new MailAddress("kqedimeliyev596@gmail.com"),
    //                Subject = "Yeni Kampanya: " + campaign.CampaignName,
    //                IsBodyHtml = true
    //            };

    //            mailMessage.To.Add(new MailAddress(subscriber.SubscribeEmail));

    //            var bodyBuilder = new StringBuilder();
    //            bodyBuilder.AppendLine("<html>");
    //            bodyBuilder.AppendLine("<body>");
    //            bodyBuilder.AppendLine("<div style='text-align:center;width:90%;margin:auto;'><h2 style='color:#E72933;'>Yeni Kampaniya: " + campaign.CampaignName + "</h2>");
    //            bodyBuilder.AppendLine("<h4>" + campaign.CampaignDescription + "</h4>");

    //            if (!string.IsNullOrEmpty(campaign.CampaignImage))
    //            {
    //                bodyBuilder.AppendLine("<div style='width:100%;margin:auto;'><img style='width:50%' src='cid:campaignImage'></div></div>");

    //                var campaignImagePath = Path.Combine("wwwroot/sekiller/", campaign.CampaignImage);
    //                var attachment = new Attachment(campaignImagePath);

    //                attachment.ContentId = "campaignImage";
    //                attachment.ContentDisposition.Inline = true;
    //                mailMessage.Attachments.Add(attachment);
    //            }

    //            bodyBuilder.AppendLine("</body>");
    //            bodyBuilder.AppendLine("</html>");

    //            mailMessage.Body = bodyBuilder.ToString();

    //            try
    //            {
    //                await _smtpClient.SendMailAsync(mailMessage);
    //            }
    //            catch (Exception ex)
    //            {
    //                // Hata yönetimi
    //            }
    //        }
    //    }
    //}


}
