using Rainfall.App.Models;

namespace Rainfall.App;

/// <summary>
/// For deserializing external api response/ add other properties if required
/// </summary>
public class ExternalApiReadingResponse
{
    public List<ExternalApiModel> items { get; set; } = [];
}
