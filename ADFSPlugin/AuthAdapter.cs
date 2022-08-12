using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace ADFSPlugin
{
    public class AuthAdapter : IAuthenticationAdapter
    {
        public IAuthenticationAdapterMetadata Metadata => new Metadata();

        public IAdapterPresentation BeginAuthentication(Claim identityClaim, HttpListenerRequest request, IAuthenticationContext context)
        {
            File.AppendAllLines("D:\\log.txt", new[] { $"BeginAuthentication:: identityClaim {identityClaim}, {identityClaim.Value}, request {request.Url} {request}, context {context.Data?.Select(x => x.Value)}" });
            return new PresentationForm();
        }

        public bool IsAvailableForUser(Claim identityClaim, IAuthenticationContext context)
        {
            File.AppendAllLines("D:\\log.txt", new[] { $"IsAvailableForUser:: identityClaim {identityClaim}, {identityClaim.Value}" });
            return true;
        }

        public void OnAuthenticationPipelineLoad(IAuthenticationMethodConfigData configData)
        {
            File.AppendAllLines("D:\\log.txt", new[] { $"OnAuthenticationPipelineLoad:: configData {configData}, {configData.Data}" });
        }

        public void OnAuthenticationPipelineUnload()
        {
            File.AppendAllLines("D:\\log.txt", new[] { $"OnAuthenticationPipelineUnload" });
        }

        public IAdapterPresentation OnError(HttpListenerRequest request, ExternalAuthenticationException ex)
        {
            File.AppendAllLines("D:\\log.txt", new[] { $"OnError:: request{request.QueryString} {request.Url}, ex {ex.Message}" });
            return new PresentationForm();
        }

        public IAdapterPresentation TryEndAuthentication(IAuthenticationContext context, IProofData proofData, HttpListenerRequest request, out Claim[] claims)
        {
            File.AppendAllLines("D:\\log.txt", new[] { $"TryEndAuthentication:: context{context.Data?.Select(x => x.Value)} {request.Url}, proofData {proofData.Properties}" });
            claims = new Claim[0];
            return new PresentationForm();
        }
    }
}
