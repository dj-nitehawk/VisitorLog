﻿using ServiceStack.FluentValidation;
using VisitorLog;

namespace Person.Retrieve
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumberRule();
            RuleFor(x => x.IDNumber).IDNumberRule();
        }
    }
}
