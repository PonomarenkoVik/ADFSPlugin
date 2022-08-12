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
    public class PresentationFormTests
    {
        [Test()]
        public void GetFormHtmlWithoutExceptionShouldReturnDefiniteForm()
        {
            //Arrange
            string name = "name";
            var form = new PresentationForm(name);

            //Act
            var res = form.GetFormHtml(0);

            //Assert
            var expected = Resources.HTMLForm.Replace(Constants.PageIntroductionText, string.Empty).Replace(Constants.Username, name);
            Assert.AreEqual(expected, res);
        }

        [Test()]
        public void GetFormHtmlWitExceptionShouldReturnDefiniteForm()
        {
            //Arrange
            string name = "name";
            var ex = new Exception();
            var form = new PresentationForm(name, ex);

            //Act
            var res = form.GetFormHtml(0);

            //Assert
            var expected = Resources.HTMLForm.Replace(Constants.PageIntroductionText, Resources.IncorrectPassword).Replace(Constants.Username, name);
            Assert.AreEqual(expected, res);
        }

        [Test()]
        public void GetFormPreRenderHtmlTest()
        {
            Assert.IsNull(new PresentationForm(string.Empty).GetFormPreRenderHtml(0));
        }

        [Test()]
        public void GetPageTitleTest()
        {
            Assert.AreEqual("ADFS Plugin", new PresentationForm(string.Empty).GetPageTitle(0));
        }
    }
}