using CityInfo.API.Services.Interfaces;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailFrom = configuration["MailSettings:MailFromAddress"];
        }

        public void Send(string to, string subject, string message)
        {
            Console.WriteLine($"Mail From {_mailFrom} To {to}, "
                + $"with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");
        }
    }
}
