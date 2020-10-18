using VisitorLog;

namespace Person.Retrieve
{
    public class Response : IResponse
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }

        public void CleanAddressIfEmpty()
        {
            if (Address.Replace(" ", "").Replace(",", "").Length == 0)
                Address = "";
        }
    }
}
