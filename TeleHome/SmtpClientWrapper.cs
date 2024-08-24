using System.Net.Mail;

namespace TeleHome
{
    public class SmtpClientWrapper : IDisposable
    {
        private SmtpClient _smtpClient;

        public SmtpClientWrapper()
        {
            _smtpClient = new SmtpClient();
            // SmtpClient ayarlarını burada yapın
        }

        public async Task SendMailAsync(MailMessage message)
        {
            await _smtpClient.SendMailAsync(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _smtpClient?.Dispose();
            }
        }
    }


}
