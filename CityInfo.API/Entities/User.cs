using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Entities
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }


        [MaxLength(50)]
        public string FirstName { get; set; }


        [MaxLength(80)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public int CityId { get; set; }

        public User(int id, string userName, string firstName, string lastName, int cityId, string password)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            CityId = cityId;
            Password = password;

        }

        #region Relations

        public City? City { get; set; }

        #endregion
    }
}
