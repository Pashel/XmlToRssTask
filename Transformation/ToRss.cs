using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace Transformation
{
    public class ToRss
    {
        private XmlReaderSettings _settings;

        public ToRss()
        {
            _settings = new XmlReaderSettings();

            _settings.Schemas.Add("http://library.by/catalog", "schema.xsd");
            _settings.ValidationEventHandler +=
                delegate (object sender, ValidationEventArgs e)
                {
                    Console.WriteLine("[{0}:{1}] {2}", e.Exception.LineNumber, e.Exception.LinePosition, e.Message);
                };

            _settings.ValidationFlags = _settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            _settings.ValidationType = ValidationType.Schema;
        }
        public bool CheckXml(Stream stream)
        {
            XmlReader reader = XmlReader.Create("CDCatalog1.xml", _settings);
            XmlDocument x = new XmlDocument();

            x.Schemas.Add("http://library.by/catalog", "schema.xsd");
            x.Validate((object sender, ValidationEventArgs e) => {});

            while (reader.Read());

            return true;
        }
    }
}
