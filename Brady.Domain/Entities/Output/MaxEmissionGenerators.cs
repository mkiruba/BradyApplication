﻿using System.Xml.Serialization;

namespace Brady.Domain.Entities.Output;

[XmlRoot(ElementName = "MaxEmissionGenerators")]
public class MaxEmissionGenerators
{
    public MaxEmissionGenerators()
    {
        Day = new List<Day>();
    }

	[XmlElement(ElementName = "Day")]
	public List<Day> Day { get; set; }
}