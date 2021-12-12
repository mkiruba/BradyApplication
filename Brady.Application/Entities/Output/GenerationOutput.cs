using System.Xml.Serialization;

namespace Brady.Application.Entities.Output;

[XmlRoot(ElementName = "GenerationOutput")]
public class GenerationOutput
{
    public GenerationOutput()
    {
        Totals = new Totals();
        MaxEmissionGenerators = new MaxEmissionGenerators();
        ActualHeatRates = new ActualHeatRates();
    }

    [XmlElement(ElementName = "Totals")]
    public Totals Totals { get; set; }

    [XmlElement(ElementName = "MaxEmissionGenerators")]
    public MaxEmissionGenerators MaxEmissionGenerators { get; set; }

    [XmlElement(ElementName = "ActualHeatRates")]
    public ActualHeatRates ActualHeatRates { get; set; }

}