using Brady.Application.Services.Interface;
using System.Xml.Serialization;

namespace Brady.Application.Services;

public class XmlReadService : IXmlReadService
{  
    public T Read<T>(string fileName)
    {
        try
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var streamReader = new StreamReader(fileName);
            var generationReport = (T)xmlSerializer.Deserialize(streamReader);
            streamReader.Close();
            return generationReport;
        }
        catch (Exception)
        {
            throw;
        }
    }
}