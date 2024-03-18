namespace Rainfall.App.Extensions;

public static class HttpResponseMessageExtension
{
    public static async Task<TRet> GetContentAsync<TRet>(this HttpResponseMessage response)
    {
        var dataStr = await response.Content.ReadAsStringAsync();
        var data = System.Text.Json.JsonSerializer.Deserialize<TRet>(dataStr);
        return data;
    }
}
