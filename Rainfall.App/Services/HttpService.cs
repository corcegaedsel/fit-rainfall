using Rainfall.App.Extensions;
using Rainfall.App.Interfaces;

namespace Rainfall.App.Services;

public class HttpService(HttpClient httpClient) : IHttpService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<TRet> GetAsync<TRet>(string url) where TRet : class
    {
        var response = await _httpClient.GetAsync(url);
        return await response.GetContentAsync<TRet>();
    }
}
