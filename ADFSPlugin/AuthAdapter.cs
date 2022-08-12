using Microsoft.IdentityServer.Web.Authentication.External;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace ADFSPlugin
{
    public class AuthAdapter : IAuthenticationAdapter
    {
        public IAuthenticationAdapterMetadata Metadata => new Metadata();

        public IAdapterPresentation BeginAuthentication(Claim identityClaim, HttpListenerRequest request, IAuthenticationContext context)
        {
            if (identityClaim == null)
            {
                var ex = new ArgumentNullException(nameof(identityClaim));
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(BeginAuthentication)}", ex);
                throw ex;
            }

            if (context == null)
            {
                var ex = new ArgumentNullException(nameof(context));
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(BeginAuthentication)}", ex);
                throw ex;
            }

            if (string.IsNullOrEmpty(identityClaim.Value))
            {
                var ex = new ArgumentException($"No user identity. Lcid: {context.Lcid}");
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(BeginAuthentication)}", ex);
                throw ex;
            }

            context.Data.Add(Constants.Identity, identityClaim.Value);

            return new PresentationForm(identityClaim.Value);
        }

        public bool IsAvailableForUser(Claim identityClaim, IAuthenticationContext context)
        {
            if (identityClaim == null)
            {
                var ex = new ArgumentNullException(nameof(identityClaim));
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(IsAvailableForUser)}", ex);
                throw ex;
            }

            if (context == null)
            {
                var ex = new ArgumentNullException(nameof(context));
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(IsAvailableForUser)}", ex);
                throw ex;
            }

            return UserValidationService.Instance.IsAvailableForUser(identityClaim, context);
        }

        public void OnAuthenticationPipelineLoad(IAuthenticationMethodConfigData configData)
        {
            EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(OnAuthenticationPipelineLoad)} has been invoked");
        }

        public void OnAuthenticationPipelineUnload()
        {
            EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(OnAuthenticationPipelineUnload)} has been invoked");
        }

        public IAdapterPresentation OnError(HttpListenerRequest request, ExternalAuthenticationException ex)
        {
            EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(OnError)}", ex);

            return new PresentationForm(string.Empty, ex);
        }

        public IAdapterPresentation TryEndAuthentication(IAuthenticationContext context, IProofData proofData, HttpListenerRequest request, out Claim[] claims)
        {
            if (null == context)
            {
                var ex = new ArgumentNullException(nameof(context));
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(TryEndAuthentication)}", ex);
                throw ex;
            }

            claims = new Claim[0];

            if (proofData?.Properties == null || !proofData.Properties.ContainsKey(Constants.Password))
            {
                var ex = new ExternalAuthenticationException($"No answer provided, Lcid: {context.Lcid}", context);
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(TryEndAuthentication)}", ex);
                throw ex;
            }

            if (!context.Data.ContainsKey(Constants.Identity))
            {
                var ex = new ExternalAuthenticationException($"No answer provided, Lcid: {context.Lcid}", context);
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(TryEndAuthentication)}::TryEndAuthentication Context does not contains userID", ex);
                throw new ArgumentOutOfRangeException(Constants.Identity);
            }

            string username = (string)context.Data[Constants.Identity];
            string password = (string)proofData.Properties[Constants.Password];

            try
            {
                if (PasswordValidationService.Instance.Validate(username, password))
                {
                    claims = new Claim[]
                    {
                        new Claim(Constants.AuthenticationMethodClaimType, Constants.UsernamePasswordMfa)
                    };

                    EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(TryEndAuthentication)} Authentication succeeded");
                    // null == authentication succeeded.
                    return null;
                }
                else
                {
                    var ex = new ExternalAuthenticationException($"Authentication failed, Lcid: {context.Lcid}", context);
                    EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(TryEndAuthentication)} Authentication failed", ex);
                    return new PresentationForm(username, ex);
                }
            }
            catch (Exception ex)
            {
                var newEx = new ExternalAuthenticationException(string.Format($"UsernamePasswordSecondFactor password validation failed due to exception {ex.Message} failed to validate user {username}, password {password}", ex), ex, context);
                EventLogger.Log($"{nameof(AuthAdapter)}.{nameof(TryEndAuthentication)} Authentication failed", newEx);
                throw newEx;
            }
        }
    }
}
