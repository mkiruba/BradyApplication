﻿using System.Xml.Serialization;

namespace Brady.Domain.Entities.Input;

[XmlRoot(ElementName = "GenerationReport")]
public class GenerationReport
{
	[XmlElement(ElementName = "Wind")]
	public Wind Wind { get; set; }

	[XmlElement(ElementName = "Gas")]
	public Gas Gas { get; set; }

	[XmlElement(ElementName = "Coal")]
	public Coal Coal { get; set; }
}