using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Claims;

namespace ADFSPlugin
{
    public class UserValidationService
    {
        private readonly static ImmutableDictionary<string, bool> Users = new Dictionary<string, bool>()
        {
            { "EnabledUser", true },
            { "DisableddUser", false }
        }.ToImmutableDictionary();

        public readonly static UserValidationService Instance = new UserValidationService();

        private UserValidationService() {}

        public bool IsAvailableForUser(Claim identityClaim, IAuthenticationContext context)
        {
            if (identityClaim == null)
            {
                throw new ArgumentNullException(nameof(identityClaim));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (Users.TryGetValue(identityClaim.Value, out var isAvailable))
            {
                return isAvailable;
            }

            return true;
        }
    }
}