using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.Models;

public class Fuels
{
    [JsonPropertyName("gas(euro/MWh)")]
    public float Gas { get; set; }
    [JsonPropertyName("kerosine(euro/MWh)")]
    public float Kerosine { get; set; }
    [JsonPropertyName("co2(euro/ton)")]
    public int CO2 { get; set; }
    [JsonPropertyName("wind(%)")]
    public int Wind { get; set; }
}
