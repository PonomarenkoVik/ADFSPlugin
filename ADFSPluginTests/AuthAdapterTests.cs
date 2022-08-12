using System;
using NUnit.Framework;
using Microsoft.IdentityServer.Web.Authentication.External;
using Moq;
using System.Security.Claims;
using System.Net;

namespace ADFSPlugin.Tests
{
    [TestFixture]
    public class AuthAdapterTests
    {
        private AuthAdapter _authAdapter;

        [SetUp]
        public void SetUp()
        {
            _authAdapter = new AuthAdapter();
        }

        [Test]
        public void BeginAuthenticationIdentityClaimIsNullThrowsArgumentNullException()
        {
            //Arrange
            Claim claim = null;
            HttpListenerRequest request = null;
            IAuthenticationContext context = new Mock<IAuthenticationContext>().Object;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => _authAdapter.BeginAuthentication(claim, request, context));
        }

        [Test]
        public void BeginAuthenticationContextIsNullThrowsArgumentNullException()
        {
            //Arrange
            Claim claim = new Claim(nameof(String), string.Empty);
            HttpListenerRequest request = null;
            IAuthenticationContext context = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => _authAdapter.BeginAuthentication(claim, request, context));
        }

        [Test]
        public void BeginAuthenticationIdentityClaimValueIsEmptyThrowsArgumentNullException()
        {
            //Arrange
            Claim claim = new Claim(nameof(String), string.Empty);
            HttpListenerRequest request = null;
            IAuthenticationContext context = new Mock<IAuthenticationContext>().Object;

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _authAdapter.BeginAuthentication(claim, request, context));
        }

        [Test]
        public void BeginAuthenticationEverythingIsSetUpSuccess()
        {
            //Arrange
            var extectedType = typeof(PresentationForm);
            var userName = "userName";
            var claim = new Claim(nameof(String), userName);
            var authenticationContext = new Mock<IAuthenticationContext>();
            var data = new System.Collections.Generic.Dictionary<string, object>();
            authenticationContext.Setup(x => x.Data).Returns(data);

            //Act
            var form = _authAdapter.BeginAuthentication(claim, null, authenticationContext.Object);

            //Assert
            Assert.IsTrue(data.TryGetValue(Constants.Identity, out var res) && res is string str && str == "userName");
            Assert.IsNotNull(form);
        }
    }
}