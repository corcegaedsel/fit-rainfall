using Rainfall.App.Models;

namespace Rainfall.App;

public class ErrorResponse
{
    public string Message { get; set; } = "";
    public List<ErrorDetailModel> Detail { get; set; } = [];
}
