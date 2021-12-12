using System.Xml.Serialization;

namespace Brady.Application.Entities.Input;

[XmlRoot(ElementName = "WindGenerator")]
public class WindGenerator : GeneratorBase
{
    [XmlElement(ElementName = "Location")]
    public string Location { get; set; }
}