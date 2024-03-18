using Rainfall.App.Models;

namespace Rainfall.App;

/// <summary>
/// Rainfall reading response
/// <para>
/// Details of a rainfall reading
/// </para>
/// </summary>
public class RainfallReadingResponse
{
    public List<RainfallReadingModel> Readings { get; set; } = [];
}
