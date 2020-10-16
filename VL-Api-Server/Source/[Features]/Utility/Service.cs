﻿using Microsoft.AspNetCore.Hosting;
using ServiceStack;
using System.IO;

namespace Utility
{
    [Route("/show-log", "GET")] //todo: protect this route with nginx or disable in production
    public class ShowLog : IReturnVoid { }

    [Route("/purge-cf-cache", "GET")] //todo: protect this route with nginx or disable in production
    public class ClearCloudflareCache : IReturnVoid { }

    public class Service : ServiceStack.Service
    {
        public IWebHostEnvironment Env { get; set; }

        public object Get(ShowLog _)
        {
            if (File.Exists("output.log"))
            {
                return new HttpResult(
                    new FileInfo(
                        Path.Combine(Env.ContentRootPath,
                        "output.log")),
                    "text/plain");
            }

            return HttpError.NotFound("Hmmmmm....");
        }
    }
}
