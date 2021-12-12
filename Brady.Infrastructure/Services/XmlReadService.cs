using Brady.Application.Services.Interface;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Brady.Infrastructure.Services;

public class XmlReadService : IXmlReadService
{
    private readonly ILogger _logger;
    public XmlReadService(ILogger logger)
    {
        _logger = logger;
    }   

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
        catch (Exception ex)
        {
            _logger.LogError($"Failed to read file {fileName}", ex);
            throw;
        }        
    }
}