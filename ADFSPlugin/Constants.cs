using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFSPlugin
{
    public static class Constants
    {
        public const string UsernamePasswordMfa = "http://schemas.microsoft.com/ws/2012/12/authmethod/usernamepasswordMFA";
        public const string AuthenticationMethodClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";

        public const string Identity = "identity";
        public const string Password = "password";


        public const string PageIntroductionText = "%PageIntroductionText%";
        public const string Username = "%Username%";
    }
}
