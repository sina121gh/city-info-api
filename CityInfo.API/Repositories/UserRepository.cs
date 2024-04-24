using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using CityInfo.API.Repositories.Interfaces;

namespace CityInfo.API.Repositories
{
    public class UserRepository : IUserRepository
    {

        #region DI

        private readonly CityInfoDbContext _context;

        public UserRepository(CityInfoDbContext context)
        {
            _context = context;
        }

        #endregion

        public User? GetUserByUserNameAndPassword(string userName, string password)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
        }
    }
}
