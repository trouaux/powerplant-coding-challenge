﻿using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.Models;

public class Response
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("p")]
    public int P { get; set; }
}
