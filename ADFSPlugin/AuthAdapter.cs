using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Net;
using System.Security.Claims;

namespace ADFSPlugin
{
    public class AuthAdapter : IAuthenticationAdapter
    {
        public IAuthenticationAdapterMetadata Metadata => throw new NotImplementedException();

        public IAdapterPresentation BeginAuthentication(Claim identityClaim, HttpListenerRequest request, IAuthenticationContext context)
        {
            throw new NotImplementedException();
        }

        public bool IsAvailableForUser(Claim identityClaim, IAuthenticationContext context)
        {
            throw new NotImplementedException();
        }

        public void OnAuthenticationPipelineLoad(IAuthenticationMethodConfigData configData)
        {
            throw new NotImplementedException();
        }

        public void OnAuthenticationPipelineUnload()
        {
            throw new NotImplementedException();
        }

        public IAdapterPresentation OnError(HttpListenerRequest request, ExternalAuthenticationException ex)
        {
            throw new NotImplementedException();
        }

        public IAdapterPresentation TryEndAuthentication(IAuthenticationContext context, IProofData proofData, HttpListenerRequest request, out Claim[] claims)
        {
            throw new NotImplementedException();
        }
    }
}
