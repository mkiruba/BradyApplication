using System.Xml.Serialization;

namespace Brady.Application.Entities.Output;

[XmlRoot(ElementName = "Totals")]
public class Totals
{
    public Totals()
    {
        Generator = new List<GeneratorBase>();
    }

    [XmlElement(ElementName = "Generator")]
    public List<GeneratorBase> Generator { get; set; }
}