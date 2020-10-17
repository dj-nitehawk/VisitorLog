﻿using ServiceStack;
using VisitorLog;

namespace Main.Establishment.SignUp.Validate
{
    [Route("/establishment/signup/validate")]
    public class Request : IRequest<Nothing>
    {
        public int Code { get; set; }
        public string Email { get; set; }
    }
}
