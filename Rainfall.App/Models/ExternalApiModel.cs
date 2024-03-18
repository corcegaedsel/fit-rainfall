namespace Rainfall.App.Models;

/// <summary>
/// For deserializing external api response/ add other properties if required
/// </summary>
public class ExternalApiModel
{
    public string dateTime { get; set; } = "";
    public decimal value { get; set; }
}
