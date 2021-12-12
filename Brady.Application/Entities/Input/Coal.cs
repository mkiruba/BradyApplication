using System.Xml.Serialization;

namespace Brady.Application.Entities.Input;

[XmlRoot(ElementName = "Coal")]
public class Coal
{
    [XmlElement(ElementName = "CoalGenerator")]
    public List<CoalGenerator> CoalGenerator { get; set; }
}