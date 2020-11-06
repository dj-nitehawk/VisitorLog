using System.Runtime.Serialization;
using VisitorLog;

namespace Person.Retrieve
{
    public class Response : IResponse
    {
        public string FullName { get; set; }

        [IgnoreDataMember]
        public string ID { get; set; }
        public string IDNumber => ID.Split('.')[0];
        public string PhoneNumber => ID.Split('.')[1];

        //NOTE: don't display address when a person is looked up by an establishment.
        //      it can be a privacy issue if any establishment can see the address of a person.
    }
}
