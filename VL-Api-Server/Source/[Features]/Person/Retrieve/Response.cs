using VisitorLog;

namespace Person.Retrieve
{
    public class Response : IResponse
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }

        //NOTE: don't display address when a person is looked up by an establishment.
        //      it can be a privacy issue if any establishment can see the address of a person.
    }
}
