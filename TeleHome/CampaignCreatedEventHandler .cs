using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text;
using TeleHome.Models;

namespace TeleHome
{
    public class CampaignCreatedEventHandler : INotificationHandler<CampaignCreatedEvent>
    {
        private readonly RmlubecoTelehomeContext _db;
        private readonly SmtpClient _smtpClient;
        public CampaignCreatedEventHandler(RmlubecoTelehomeContext db, SmtpClient smtpClient)
        {
            _db = db;
            _smtpClient = smtpClient;
        }
        public async Task Handle(CampaignCreatedEvent notification, CancellationToken cancellationToken)
        {
            var kampanya = notification.Campaign;
            var subscribers = _db.Subscribes.ToList();

            await Task.WhenAll(subscribers.Select(async (subscriber) =>
            {
                using (var smtpClient = new SmtpClient("plesk3005.my-hosting-panel.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("info@telehome.az", "TeleHome@123");
                    smtpClient.EnableSsl = true;
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("info@telehome.az"),
                        Subject = "Yeni Kampanya: " + kampanya.CampaignName,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(new MailAddress(subscriber.SubscribeEmail));

                    var bodyBuilder = new StringBuilder();
                    bodyBuilder.AppendLine("<html>");
                    bodyBuilder.AppendLine("<body>");
                    bodyBuilder.AppendLine("<div style='text-align:center;width:90%;margin:auto;'><h2 style='color:#E72933;'>Yeni Kampaniya: " + kampanya.CampaignName + "</h2>");
                    bodyBuilder.AppendLine("<h4>" + kampanya.CampaignDescription + "</h4>");

                    if (!string.IsNullOrEmpty(kampanya.CampaignImage))
                    {
                        bodyBuilder.AppendLine("<div style='width:100%;margin:auto;'><img style='width:50%' src='cid:campaignImage'></div></div>");

                        var campaignImagePath = Path.Combine("wwwroot/sekiller/", kampanya.CampaignImage);
                        var attachment = new Attachment(campaignImagePath);

                        attachment.ContentId = "campaignImage";
                        attachment.ContentDisposition.Inline = true;
                        mailMessage.Attachments.Add(attachment);
                    }

                    bodyBuilder.AppendLine("</body>");
                    bodyBuilder.AppendLine("</html>");

                    mailMessage.Body = bodyBuilder.ToString();

                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        // Hata yönetimi
                    }
                }
            }));
        }
    }
}
