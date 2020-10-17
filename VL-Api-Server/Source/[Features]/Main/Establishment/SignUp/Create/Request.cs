using ServiceStack;
using VisitorLog;

namespace Main.Establishment.SignUp.Create
{
    [Route("/establishment/signup/create")]
    public class Request : Model, IRequest<Dom.Establishment, Nothing>
    {
        public string Email { get; set; }
        public string EmailConfirmation { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public Dom.Establishment ToEntity()
        {
            return new Dom.Establishment
            {
                Address = new Dom.Address
                {
                    City = City,
                    CountryCode = CountryCode,
                    State = State,
                    Street = Street.TitleCase(),
                    ZipCode = ZipCode.Trim()
                },
                ContactDesignation = ContactDesignation.TitleCase(),
                ContactName = ContactName.TitleCase(),
                Email = Email.LowerCase(),
                GoogleMapURL = MapLocation.EmbedCodeToURL(GoogleMapURL),
                Location = MapLocation.Get2DCoordinates(GoogleMapURL),
                Name = Name.TitleCase(),
                PasswordHash = Password.SaltedHash(),
                PhoneNumber = PhoneNumber,
                Type = Type
            };
        }
    }
}
