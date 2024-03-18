// using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Rainfall.API.Controllers;
using Rainfall.App.Services;

namespace Rainfall.Tests;

public class Tests
{
    [Test]
    public void GetStationReadings_Ok200()
    {
        const string EXT_API_URL = @"https://environment.data.gov.uk/flood-monitoring/id/stations/{0}/readings?_sorted&_limit={1}";

        var mockLogger = new Mock<ILogger<RainfallController>>();
        var logger = mockLogger.Object;
        
        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x => x.Value).Returns(EXT_API_URL);

        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(x => x.GetSection("ExternalApiUri")).Returns(mockSection.Object);

        var httpService = new HttpService(new HttpClient());

        var apiCtrl = new RainfallController(logger, httpService, mockConfig.Object);

        var res = apiCtrl.GetRainfallAsync();
        var objRes = res.Result as ObjectResult;

        Assert.That(objRes, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void GetStationReadings_BadRequest400()
    {
        const string EXT_API_URL = @"https://environment.data.gov.uk/flood-monitoring/id/stations/{0}/readings?_sorted&_limit={1}";

        var mockLogger = new Mock<ILogger<RainfallController>>();
        var logger = mockLogger.Object;

        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x => x.Value).Returns(EXT_API_URL);

        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(x => x.GetSection("ExternalApiUri")).Returns(mockSection.Object);

        var httpService = new HttpService(new HttpClient());

        var apiCtrl = new RainfallController(logger, httpService, mockConfig.Object);

        var res = apiCtrl.GetRainfallAsync("E7050", 0);
        var objRes = res.Result as ObjectResult;

        Assert.That(objRes, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public void GetStationReadings_NotFound404()
    {
        const string EXT_API_URL = @"https://environment.data.gov.uk/flood-monitoring/id/stations/{0}/readings?_sorted&_limit={1}";

        var mockLogger = new Mock<ILogger<RainfallController>>();
        var logger = mockLogger.Object;

        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x => x.Value).Returns(EXT_API_URL);

        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(x => x.GetSection("ExternalApiUri")).Returns(mockSection.Object);

        var httpService = new HttpService(new HttpClient());

        var apiCtrl = new RainfallController(logger, httpService, mockConfig.Object);

        var res = apiCtrl.GetRainfallAsync("E7050-404");
        var objRes = res.Result as ObjectResult;

        Assert.That(objRes, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public void GetStationReadings_InternalServerError500()
    {
        var mockLogger = new Mock<ILogger<RainfallController>>();
        var logger = mockLogger.Object;

        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(x => x.Value).Returns("");

        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(x => x.GetSection("ExternalApiUri")).Returns(mockSection.Object);

        var httpService = new HttpService(new HttpClient());

        var apiCtrl = new RainfallController(logger, httpService, mockConfig.Object);

        var res = apiCtrl.GetRainfallAsync("E7050");
        var objRes = res.Result as ObjectResult;

        Assert.That(objRes.StatusCode, Is.EqualTo(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError));
    }
}