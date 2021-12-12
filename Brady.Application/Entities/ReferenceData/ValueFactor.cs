using System.Xml.Serialization;

namespace Brady.Application.Entities.ReferenceData;

[XmlRoot(ElementName = "ValueFactor")]
public class ValueFactor
{
    [XmlElement(ElementName = "High")]
    public decimal High { get; set; }

    [XmlElement(ElementName = "Medium")]
    public decimal Medium { get; set; }

    [XmlElement(ElementName = "Low")]
    public decimal Low { get; set; }
}