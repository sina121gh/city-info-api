namespace CityInfo.API.Services.Interfaces
{
    public interface IMailService
    {
        void Send(string to, string subject, string htmlString);
    }
}