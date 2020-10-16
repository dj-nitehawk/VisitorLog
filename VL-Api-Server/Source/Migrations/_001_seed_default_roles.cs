﻿using Dom;
using MongoDB.Entities;
using VisitorLog;
using VisitorLog.Auth;
using System.Linq;
using System.Threading.Tasks;

namespace Migrations
{
    public class _001_seed_default_roles : IMigration
    {
        public async Task UpgradeAsync()
        {
            await DB.DeleteAsync<Role>(_ => true);

            var manager = new Role
            {
                ID = "5ee2298f74057809db6cf5bf",
                SystemRole = true,
                Name = "Manager",
                Permissions = new[] //ONLY THE FOLLOWING:
                {
                    Allow.Account_Read,
                    Allow.Account_Update,
                    Allow.Account_Delete,
                    Allow.Employee_Create,
                    Allow.Employee_Read,
                    Allow.Employee_Update,
                    Allow.Employee_Delete,
                }.Cast<ushort>()
            };

            var employee = new Role
            {
                ID = "5ee2298f74057809db6cf5c0",
                SystemRole = true,
                Name = "Employee",
                Permissions = default(Allow).All().Except(new[] //EVERYTHING EXCLUDING FOLLOWING:
                {
                    Allow.Employee_Create,
                    Allow.Employee_Delete,
                }).Cast<ushort>()
            };

            await DB.SaveAsync(new[] { manager, employee });
        }
    }
}
