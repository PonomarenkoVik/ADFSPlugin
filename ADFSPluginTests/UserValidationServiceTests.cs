using NUnit.Framework;
using ADFSPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityServer.Web.Authentication.External;
using Moq;

namespace ADFSPlugin.Tests
{
    [TestFixture()]
    public class UserValidationServiceTests
    {
        [Test()]
        public void IsAvailableForUserClaimIsNullThrowException()
        {
            //Arrange
            Claim claim = null;
            IAuthenticationContext context = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => UserValidationService.Instance.IsAvailableForUser(claim, context));
        }

        [Test()]
        public void IsAvailableForUserContextIsNullThrowException()
        {
            //Arrange
            Claim claim = new Claim(nameof(String), "customString");
            IAuthenticationContext context = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => UserValidationService.Instance.IsAvailableForUser(claim, context));
        }

        [Test()]
        public void IsAvailableForUserFlowForCustomUserShouldReturnTrue()
        {
            //Arrange
            Claim claim = new Claim(nameof(String), "customString");
            IAuthenticationContext context = new Mock<IAuthenticationContext>().Object;

            //Act
            //Assert
            Assert.IsTrue(UserValidationService.Instance.IsAvailableForUser(claim, context));
        }

        [Test()]
        public void IsAvailableForUserFlowForEnabledUserShouldReturnTrue()
        {
            //Arrange
            Claim claim = new Claim(nameof(String), "EnabledUser");
            IAuthenticationContext context = new Mock<IAuthenticationContext>().Object;

            //Act
            //Assert
            Assert.IsTrue(UserValidationService.Instance.IsAvailableForUser(claim, context));
        }

        [Test()]
        public void IsAvailableForUserFlowForDisabledUserShouldReturnFalse()
        {
            //Arrange
            Claim claim = new Claim(nameof(String), "DisableddUser");
            IAuthenticationContext context = new Mock<IAuthenticationContext>().Object;

            //Act
            //Assert
            Assert.IsFalse(UserValidationService.Instance.IsAvailableForUser(claim, context));
        }
    }
}