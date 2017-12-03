using System;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Transformation;
using XmlToRssTask.Controllers;
using System.Web;
using System.IO;

namespace XmlToRssTask.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ITransformation> _xmlFormatter;
        private Mock<HttpPostedFileBase> _file;

        [SetUp]
        public void SetUp()
        {
            _xmlFormatter = new Mock<ITransformation>();
            _xmlFormatter.Setup(x => x.Convert(It.IsAny<string>())).Returns("hello world");
            _file = new Mock<HttpPostedFileBase>();
        }

        [Test]
        public void UploadXml_WithCorrectXmlFile_GetTranformedFile()
        {
            // Arrange
            _xmlFormatter.Setup(x => x.CheckXml(It.IsAny<string>())).Returns(true);
            HomeController controller = new HomeController(_xmlFormatter.Object);

            // Act
            var result = controller.UploadXml(_file.Object) as JsonResult;

            // Arrange
            Assert.IsNotNull(result);
            Assert.AreEqual("hello world", result.Data);
        }

        [Test]
        public void UploadXml_WithIncorrectXmlFile_FileDoesNotFitScheme()
        {
            // Arrange
            _xmlFormatter.Setup(x => x.CheckXml(It.IsAny<string>())).Returns(false);
            HomeController controller = new HomeController(_xmlFormatter.Object);

            // Act
            var result = controller.UploadXml(_file.Object) as HttpStatusCodeResult;

            // Arrange
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public void UploadXml_WithNotXmlFile_FileDoesNotFitScheme()
        {
            // Arrange
            _xmlFormatter.Setup(x => x.SetXmlFile(It.IsAny<Stream>())).Throws(new Exception("Incorrect xml"));
            HomeController controller = new HomeController(_xmlFormatter.Object);

            // Act & Arrange
            try {
                controller.UploadXml(_file.Object);
            } catch(Exception ex) { 
                Assert.AreEqual("Incorrect xml", ex.Message);
            }
        }
    }
}
