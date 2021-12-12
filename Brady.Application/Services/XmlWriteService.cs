using Brady.Application.Services.Interface;
using System.Xml.Serialization;

namespace Brady.Application.Services;

public class XmlWriteService<T> : IXmlWriteService<T>
{    
    public void Write(T input, string outputFilePath)
    {
        try
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var streamWriter = new StreamWriter(outputFilePath);
            xmlSerializer.Serialize(streamWriter, input);
            streamWriter.Close();
        }
        catch (Exception)
        {
            throw;
        }
    }
}