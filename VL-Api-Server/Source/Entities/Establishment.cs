using MongoDB.Entities;

namespace Dom
{
    public class Establishment : Entity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Address Address { get; set; }
        public string GoogleMapURL { get; set; }
        public Coordinates2D Location { get; set; }
        public string ContactName { get; set; }
        public string ContactDesignation { get; set; }
        public string PhoneNumber { get; set; }

        [Preserve] public string Email { get; set; }
        [Preserve] public string PasswordHash { get; set; }
        [Preserve] public bool IsEmailVerified { get; set; }

        static Establishment()
        {
            DB.Index<Establishment>()
              .Key(e => e.Email, KeyType.Ascending)
              .Option(e => e.Unique = true)
              .CreateAsync();
        }
    }
}