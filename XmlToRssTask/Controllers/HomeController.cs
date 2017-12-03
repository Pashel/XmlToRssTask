using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Transformation;
using XmlToRssTask.Filters;

namespace XmlToRssTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransformation _xmlFormatter;

        public HomeController(ITransformation xmlFormatter)
        {
            _xmlFormatter = xmlFormatter;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AjaxException]
        public ActionResult UploadXml(HttpPostedFileBase file)
        {
            _xmlFormatter.SetXmlFile(file.InputStream);

            // Check if file fit to scheme
            string schema = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings.Get("SchemaName") ?? String.Empty);
            var status = _xmlFormatter.CheckXml(schema);
            if (!status) {
                return new HttpStatusCodeResult(500, "Validation failed. Uploaded xml file has incorrect format.");
            }

            // Apply transformation
            string xsltFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings.Get("XsltFileName") ?? String.Empty);
            var result = _xmlFormatter.Convert(xsltFile);

            return Json(result);
        }
    }

}