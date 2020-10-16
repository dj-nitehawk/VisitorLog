﻿using System.Collections.Generic;
using System.Linq;
using VisitorLog.Auth;

namespace VisitorLog
{
    /// <summary>
    /// Abstract class for login responses
    /// </summary>
    public abstract class LoginResponse : IResponse
    {
        /// <summary>
        /// The token
        /// </summary>
        public JwtToken Token { get; set; }

        /// <summary>
        /// The list of permissions for the user
        /// </summary>
        public IEnumerable<string> PermissionSet { get; set; }

        /// <summary>
        /// Generates the JWT token for the given user with a set of permissions and roles
        /// </summary>
        /// <param name="userSession">The user session to create the token for</param>
        /// <param name="permissions">The permissions to assign for the user</param>
        /// <param name="roles">The roles to assign the user</param>
        public void SignIn(UserSession userSession, IEnumerable<Allow> permissions, string[] roles = default)
        {
            Token = Authentication.GenerateToken(userSession, permissions, roles);
            PermissionSet = permissions
                .Select(p => p.ToString())
                .OrderBy(s => s);
        }
    }
}
