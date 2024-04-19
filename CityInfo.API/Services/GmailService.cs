using System.Net.Mail;
using System.Net;
using CityInfo.API.Services.Interfaces;

namespace CityInfo.API.Services
{
    public class GmailService : IMailService
    {
        string _mailTo = "sina121gh@gmail.com";
        string _mailFrom = "sina121gh@gmail.com";

        public void Send(string to, string subject, string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("sina121gh@gmail.com");
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("sina121gh@gmail.com", "qojo yxrd zmwy yzzq");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
