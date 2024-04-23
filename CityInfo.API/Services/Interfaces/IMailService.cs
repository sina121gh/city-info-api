namespace CityInfo.API.Services.Interfaces
{
    public interface IMailService
    {
        Task Send(string to, string subject, string htmlString);
    }
}