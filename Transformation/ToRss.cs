using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace Transformation
{
    public class ToRss : ITransformation
    {
        private XmlDocument _xDoc;
        private string _targetNamespace;

        public void SetXmlFile(Stream stream)
        {
            _xDoc = new XmlDocument();
            _xDoc.Load(stream);

            var elem = _xDoc.DocumentElement?.FirstChild;
            _targetNamespace = elem?.NamespaceURI;
        }

        private void ThrowIsXmlNotSet()
        {
            if (_xDoc == null || String.IsNullOrEmpty(_targetNamespace)) {
                throw new ValidationException("Please check that you set xml file for transformation.");
            }
        }

        public bool CheckXml(string schema)
        {
            ThrowIsXmlNotSet();

            _xDoc.Schemas.Add(_targetNamespace, schema);

            bool isCorrect = true;
            _xDoc.Validate((object sender, ValidationEventArgs e) => {
                isCorrect = false;
            });

            return isCorrect;
        }

        public string Convert(string xsltFile)
        {
            ThrowIsXmlNotSet();

            var xsl = new XslCompiledTransform();
            xsl.Load(xsltFile);

            var xlsParams = new XsltArgumentList();
            xlsParams.AddParam("Date", "", DateTime.Now);

            using (Stream outStream = new MemoryStream()) {
                xsl.Transform(_xDoc, xlsParams, outStream);
                outStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outStream, Encoding.UTF8)) {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
