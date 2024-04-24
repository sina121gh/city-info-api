using CityInfo.API.Entities;

namespace CityInfo.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserByUserNameAndPassword(string userName, string password);
    }
}
