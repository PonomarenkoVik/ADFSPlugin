using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFSPlugin
{
    internal class Metadata : IAuthenticationAdapterMetadata
    {
        public string[] AuthenticationMethods => new[] { "http://example.com/myauthenticationmethod1", "http://example.com/myauthenticationmethod2" };

        public string[] IdentityClaims => new[] { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn" };

        public string AdminName => "My ADFS Example";

        public int[] AvailableLcids => new[] { new CultureInfo("en-us").LCID };

        public Dictionary<int, string> Descriptions
        {
            get
            {
                var descriptions = new Dictionary<int, string>();
                descriptions.Add(new CultureInfo("en-us").LCID, "Description of My Example ADFS Plugin for end users (en)");
                return descriptions;
            }
        }

        public Dictionary<int, string> FriendlyNames
        {
            get
            {
                var friendlyNames = new Dictionary<int, string>();
                friendlyNames.Add(new CultureInfo("en-us").LCID, "Friendly name of My Example ADFS Plugin for end users (en)");
                return friendlyNames;
            }
        }

        public bool RequiresIdentity => true;
    }
}
