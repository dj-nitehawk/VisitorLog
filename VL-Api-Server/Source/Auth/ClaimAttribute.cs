﻿using ServiceStack;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace VisitorLog.Auth
{
    /// <summary>
    /// Use this attribute to only allow users who has the given ClaimTypes
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class NeedAttribute : AuthenticateAttribute
    {
        private readonly string[] claims;
        private readonly bool allowAny;

        /// <summary>
        /// Restrict users by ClaimType
        /// </summary>
        /// <param name="applyTo">Specify which http verbs this applies to</param>
        /// <param name="claimTypes">All the claim types the user must have in order to be allowed access</param>
        public NeedAttribute(ApplyTo applyTo, params string[] claimTypes)
        {
            claims = claimTypes;
            ApplyTo = applyTo;
            Priority = (int)RequestFilterPriority.RequiredRole;
        }

        /// <summary>
        /// Restrict users by ClaimType
        /// </summary>
        /// <param name="claimTypes">All the claim types the user must have in order to be allowed access</param>
        public NeedAttribute(params string[] claimTypes)
        {
            claims = claimTypes;
            ApplyTo = ApplyTo.All;
            Priority = (int)RequestFilterPriority.RequiredRole;
        }

        /// <summary>
        /// Restrict users by ClaimType
        /// </summary>
        /// <param name="anyClaim">Specify if having at least one of the claim types allows access</param>
        /// <param name="claimTypes">The claim types</param>
        public NeedAttribute(bool anyClaim, params string[] claimTypes)
        {
            claims = claimTypes;
            ApplyTo = ApplyTo.All;
            Priority = (int)RequestFilterPriority.RequiredRole;
            allowAny = anyClaim;
        }

        /// <summary>
        /// Restrict users by ClaimType
        /// </summary>
        /// <param name="applyTo">Specify which http verbs this applies to</param>
        /// <param name="anyClaim">Specify if having at least one of the claim types allows access</param>
        /// <param name="claimTypes">The claim types</param>
        public NeedAttribute(ApplyTo applyTo, bool anyClaim, params string[] claimTypes)
        {
            claims = claimTypes;
            ApplyTo = applyTo;
            Priority = (int)RequestFilterPriority.RequiredRole;
            allowAny = anyClaim;
        }

        public override async Task ExecuteAsync(ServiceStack.Web.IRequest req, ServiceStack.Web.IResponse res, object requestDto)
        {
            if (HostContext.AppHost.HasValidAuthSecret(req))
                return;

            await base.ExecuteAsync(req, res, requestDto); //first check if session is authenticated

            if (res.IsClosed)
                return; //AuthenticateAttribute already closed the request (ie auth failed)

            if (allowAny && claims.Intersect(req.SessionAs<UserSession>().Claims.Select(c => c.Key)).Any())
                return; //user does have at least one required claim
            else if (!claims.Except(req.SessionAs<UserSession>().Claims.Select(c => c.Key)).Any())
                return; //user has all required claims

            if (DoHtmlRedirectAccessDeniedIfConfigured(req, res))
                return;

            res.StatusCode = (int)HttpStatusCode.Forbidden;
            res.StatusDescription = "The user doesn't have the required claim(s)!";
            await HostContext.AppHost.HandleShortCircuitedErrors(req, res, requestDto);
        }

        protected bool Equals(NeedAttribute other)
        {
            return base.Equals(other) && string.Equals(claims, other.claims);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NeedAttribute)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), claims);
        }
    }
}
