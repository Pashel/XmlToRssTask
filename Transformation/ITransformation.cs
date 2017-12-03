using System.IO;

namespace Transformation
{
    public interface ITransformation
    {
        void SetXmlFile(Stream stream);
        bool CheckXml(string schema);
        string Convert(string xsltFile);
    }
}
