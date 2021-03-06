using System.Xml.Serialization;

namespace Brady.Application.Entities.Input;

[XmlRoot(ElementName = "GasGenerator")]
public class GasGenerator : GeneratorBase
{
    [XmlElement(ElementName = "EmissionsRating")]
    public decimal EmissionsRating { get; set; }
}