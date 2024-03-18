namespace Rainfall.App.Models;

/// <summary>
/// Rainfall reading
/// <para>
/// Details of a rainfall reading
/// </para>
/// </summary>
public class RainfallReadingModel
{
    public string DateMeasured { get; set; } = "";
    public decimal AmountMeasured { get; set; }
}
