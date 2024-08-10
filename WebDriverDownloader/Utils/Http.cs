namespace WebDriverDownloader.Utils;

internal static class Http
{
    internal static async Task<string> GetString(string uri)
    {
        using var http = new HttpClient();
        return await http.GetStringAsync(uri);
    }
}
