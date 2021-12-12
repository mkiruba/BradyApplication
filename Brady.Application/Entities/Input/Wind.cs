using System.Xml.Serialization;

namespace Brady.Application.Entities.Input;

[XmlRoot(ElementName = "Wind")]
public class Wind
{
    [XmlElement(ElementName = "WindGenerator")]
    public List<WindGenerator> WindGenerator { get; set; }
}