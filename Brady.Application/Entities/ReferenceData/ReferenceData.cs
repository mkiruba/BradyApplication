using System.Xml.Serialization;

namespace Brady.Application.Entities.ReferenceData;

[XmlRoot(ElementName = "ReferenceData")]
public class ReferenceData
{
    [XmlElement(ElementName = "Factors")]
    public Factors Factors { get; set; }
}