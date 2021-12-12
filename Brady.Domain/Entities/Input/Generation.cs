using System.Xml.Serialization;

namespace Brady.Domain.Entities.Input;

[XmlRoot(ElementName = "Generation")]
public class Generation
{
	[XmlElement(ElementName = "Day")]
	public List<Day> Day { get; set; }
}