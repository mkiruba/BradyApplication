using System.Xml.Serialization;

namespace Brady.Application.Entities.Output;

[XmlRoot(ElementName = "Day")]
public class Day
{
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Date")]
    public string Date { get; set; }

    [XmlElement(ElementName = "Emission")]
    public decimal Emission { get; set; }
}