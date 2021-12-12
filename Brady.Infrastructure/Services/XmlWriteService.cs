using Brady.Application.Services.Interface;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Brady.Infrastructure.Services;

public class XmlWriteService<T> : IXmlWriteService<T>
{
    private readonly ILogger _logger;
    public XmlWriteService(ILogger logger)
    {
        _logger = logger;
    }

    public void Write(T input, string outputFilePath)
    {
        try
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var streamWriter = new StreamWriter(outputFilePath);
            xmlSerializer.Serialize(streamWriter, input);
            streamWriter.Close();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to write file {outputFilePath}", ex);
            throw;
        }        
    }
}