using Microsoft.AspNetCore.Mvc;
using Rainfall.App;
using Rainfall.App.Interfaces;

namespace Rainfall.API.Controllers;

[ApiController]
[Route("rainfall")]
public class RainfallController(ILogger<RainfallController> logger, IHttpService httpService, IConfiguration configuration) : ControllerBase
{
    private readonly ILogger<RainfallController> _logger = logger;
    private readonly IHttpService _httpService = httpService;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Get rainfall readings by station Id
    /// <para>
    /// Retrieve the latest readings for the specified stationId
    /// </para>
    /// </summary>
    /// <param name="id">Station Id</param>
    /// <param name="count">Between 1-100</param>
    /// <returns></returns>
    [HttpGet("id/{id}/readings")]
    public async Task<IActionResult> GetRainfallAsync(string id = "E7050", int count = 10)
    {
        try
        {
            var apiUriRaw = _configuration.GetValue<string>("ExternalApiUri") ?? "";
            var apiUrl = string.Format(apiUriRaw, id, count);
            if (string.IsNullOrWhiteSpace(apiUrl))
            {
                throw new Exception("External API can not be null or empty"); // simulate internal server error
            }
            else
            {
                if (count < 1 || count > 100)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Message = "Invalid value for Count",
                        Detail = [new App.Models.ErrorDetailModel {
                            PropertyName = "Count",
                            Message = "Count value should be 1-100"
                        }]
                    });
                }
            }

            var resRaw = await _httpService.GetAsync<ExternalApiReadingResponse>(apiUrl);
            if (resRaw == null || resRaw.items?.Count == 0) {
                return NotFound(new ErrorResponse
                {
                    Message = "No readings found for the specified stationId",
                    Detail = [new App.Models.ErrorDetailModel {
                        PropertyName = "Items",
                        Message = "Empty data"
                    }]
                });
            }

            var res = new RainfallReadingResponse();
            foreach (var item in resRaw.items ?? [])
            {
                res.Readings.Add(new App.Models.RainfallReadingModel
                {
                    AmountMeasured = item.value,
                    DateMeasured = item.dateTime
                });
            }

            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.StackTrace}{Environment.NewLine}{ex.Message}");
            return StatusCode(500, new ErrorResponse
            {
                Message = "Internal server error",
                Detail = [new App.Models.ErrorDetailModel {
                    PropertyName = "Exception",
                    Message = "See logs for more details"
                }]
            });
        }
    }
}
