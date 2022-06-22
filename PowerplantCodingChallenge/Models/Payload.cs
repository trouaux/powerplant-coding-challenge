﻿using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.Models;

public class Payload
{
    [JsonPropertyName("load")]
    public int Load { get; set; }
    [JsonPropertyName("fuels")]
    public Fuels Fuels { get; set; }
    [JsonPropertyName("powerplants")]
    public List<Powerplant> Powerplants { get; set; }
}
