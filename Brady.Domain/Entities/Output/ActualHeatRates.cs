﻿using System.Xml.Serialization;

namespace Brady.Domain.Entities.Output;

[XmlRoot(ElementName = "ActualHeatRates")]
public class ActualHeatRates
{
    public ActualHeatRates()
    {
        ActualHeatRate = new List<ActualHeatRate>();
    }

	[XmlElement(ElementName = "ActualHeatRate")]
	public List<ActualHeatRate> ActualHeatRate { get; set; }
}