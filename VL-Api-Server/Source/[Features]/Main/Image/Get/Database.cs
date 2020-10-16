﻿using MongoDB.Entities;
using VisitorLog;
using System.IO;
using System.Threading.Tasks;

namespace Main.Image.Get
{
    public class Database : IDatabase
    {
        public Task DownloadAsync(string id, Stream destination)
        {
            return DB.File<Dom.Image>(id)
                     .DownloadWithTimeoutAsync(destination, 30);
        }
    }
}
