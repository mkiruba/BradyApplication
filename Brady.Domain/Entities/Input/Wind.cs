using System.Xml.Serialization;

namespace Brady.Domain.Entities.Input;

[XmlRoot(ElementName = "Wind")]
public class Wind
{
	[XmlElement(ElementName = "WindGenerator")]
	public List<WindGenerator> WindGenerator { get; set; }
}