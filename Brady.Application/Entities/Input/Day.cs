using System.Xml.Serialization;

namespace Brady.Application.Entities.Input;

[XmlRoot(ElementName = "Day")]
public class Day
{
    [XmlElement(ElementName = "Date")]
    public string Date { get; set; }

    [XmlElement(ElementName = "Energy")]
    public decimal Energy { get; set; }

    [XmlElement(ElementName = "Price")]
    public decimal Price { get; set; }
}