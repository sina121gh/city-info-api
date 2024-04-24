namespace CityInfo.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public User(int id, string userName, string firstName, string lastName, string city)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }
}
