using System.Xml.Serialization;

namespace Brady.Domain.Entities.ReferenceData;

[XmlRoot(ElementName = "ReferenceData")]
public class ReferenceData
{
	[XmlElement(ElementName = "Factors")]
	public Factors Factors { get; set; }
}