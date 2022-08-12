using NUnit.Framework;
using ADFSPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADFSPlugin.Tests
{
    [TestFixture()]
    public class PasswordValidationServiceTests
    {
        [Test()]
        public void ValidateTest()
        {
            Assert.IsFalse(PasswordValidationService.Instance.Validate(null, null));
            Assert.IsTrue(PasswordValidationService.Instance.Validate(null, null));
        }
    }
}