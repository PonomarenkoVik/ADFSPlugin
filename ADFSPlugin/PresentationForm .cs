using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFSPlugin
{
    internal class PresentationForm : IAdapterPresentationForm
    {
        public string GetFormHtml(int lcid) => Resources.HTMLForm;

        public string GetFormPreRenderHtml(int lcid) => null;
       
        public string GetPageTitle(int lcid) => "ADFS Plugin";
    }
}
