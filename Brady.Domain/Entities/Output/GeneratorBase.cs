﻿using System.Xml.Serialization;

namespace Brady.Domain.Entities.Output;

[XmlRoot(ElementName = "Generator")]
public class GeneratorBase
{
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Total")]
    public decimal Total { get; set; }
}
