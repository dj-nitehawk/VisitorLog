﻿using MongoDB.Entities;
using VisitorLog;
using System.Threading.Tasks;

namespace Main.Account.Get
{
    public class Database : IDatabase
    {
        public Task<Dom.Account> GetAccountAsync(string accountID)
        {
            return DB.Find<Dom.Account>()
                     .Match(a => a.ID == accountID)
                     .ProjectExcluding(a => new
                     {
                         a.PasswordHash,
                         a.EmailVerificationCode
                     })
                     .ExecuteSingleAsync();
        }
    }
}
