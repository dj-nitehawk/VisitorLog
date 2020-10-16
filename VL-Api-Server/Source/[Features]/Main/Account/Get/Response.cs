﻿using VisitorLog;

namespace Main.Account.Get
{
    public class Response : Model, IResponse<Dom.Account>
    {
        public bool IsEmailVerified { get; set; }

        public void FromEntity(Dom.Account a)
        {
            City = a.Address.City;
            CountryCode = a.Address.CountryCode;
            EmailAddress = a.Email;
            FirstName = a.FirstName;
            ID = a.ID;
            IsEmailVerified = a.IsEmailVerified;
            LastName = a.LastName;
            Mobile = a.Mobile;
            Password = "";
            State = a.Address.State;
            Street = a.Address.Street;
            Title = a.Title;
            ZipCode = a.Address.ZipCode;
        }
    }
}
