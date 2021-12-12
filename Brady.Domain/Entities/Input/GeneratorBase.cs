using System.Xml.Serialization;

namespace Brady.Domain.Entities.Input;

public class GeneratorBase
{
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Generation")]
    public Generation Generation { get; set; }
}
