using System.Xml.Serialization;

namespace Brady.Domain.Entities.Input;

[XmlRoot(ElementName = "CoalGenerator")]
public class CoalGenerator : GeneratorBase
{
	[XmlElement(ElementName = "TotalHeatInput")]
	public decimal TotalHeatInput { get; set; }

	[XmlElement(ElementName = "ActualNetGeneration")]
	public decimal ActualNetGeneration { get; set; }

	[XmlElement(ElementName = "EmissionsRating")]
	public decimal EmissionsRating { get; set; }
}