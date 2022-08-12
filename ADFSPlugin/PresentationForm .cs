using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFSPlugin
{
    public class PresentationForm : IAdapterPresentationForm
    {

        private readonly string _username;
        private readonly Exception _exception;
        public PresentationForm(string username)
        {
            _username = username;
        }

        public PresentationForm(string username, Exception ex) : this(username)
        {
            _exception = ex;
        }

        public string GetFormHtml(int lcid)
        {
            var form = _exception == null ? Resources.HTMLForm.Replace(Constants.PageIntroductionText, string.Empty) :
                Resources.HTMLForm.Replace(Constants.PageIntroductionText, Resources.IncorrectPassword);

            return form.Replace(Constants.Username, _username);
        }

        public string GetFormPreRenderHtml(int lcid) => null;
       
        public string GetPageTitle(int lcid) => "ADFS Plugin";
    }
}
