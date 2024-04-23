using System.Net.Mail;
using System.Net;
using CityInfo.API.Services.Interfaces;

namespace CityInfo.API.Services
{
    public class GmailService : IMailService
    {
        private readonly string _mailFrom = string.Empty;
        private readonly string _mailTo = string.Empty;
        public GmailService(IConfiguration configuration)
        {
            _mailFrom = configuration["MailSettings:MailFromAddress"];
            _mailTo = configuration["MailSettings:MailToAddress"];
        }

        public async Task Send(string to, string subject, string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_mailFrom);
                message.To.Add(new MailAddress(_mailTo));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_mailFrom, "qojo yxrd zmwy yzzq");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
            }
            catch (Exception) { }
        }
    }
}
