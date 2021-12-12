using System.Xml.Serialization;

namespace Brady.Domain.Entities.Input;

[XmlRoot(ElementName = "Gas")]
public class Gas
{
	[XmlElement(ElementName = "GasGenerator")]
	public List<GasGenerator> GasGenerator { get; set; }
}