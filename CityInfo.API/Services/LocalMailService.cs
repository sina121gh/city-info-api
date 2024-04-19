using CityInfo.API.Services.Interfaces;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        public void Send(string to, string subject, string message)
        {
            Console.WriteLine($"Mail From logger To {to}, "
                + $"with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");
        }
    }
}
