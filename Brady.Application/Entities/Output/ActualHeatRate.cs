using System.Xml.Serialization;

namespace Brady.Application.Entities.Output;

[XmlRoot(ElementName = "ActualHeatRate")]
public class ActualHeatRate
{
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "HeatRate")]
    public decimal HeatRate { get; set; }
}