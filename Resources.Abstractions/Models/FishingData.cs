namespace Resources.Abstractions.Models;

public class FishingData
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string? Method { get; set; }
    public string? Bait { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? FishSpecies { get; set; }
    public double FishWeight { get; set; }
    public double FishLength { get; set; }
    public double Temperature { get; set; }
    public string? Weather { get; set; }
}

