using System.IO.Compression;
using WebDriverDownloader.DriverDownloader;

namespace WebDriverDownloader.Chrome;

internal class ChromeDriverDownloader : IDriverDownloader
{
    public async Task DownloadDriver(Uri uri, string filePath)
    {
        using var zip = await DownloadZip(uri);
        var driverEntry = zip.Entries.FirstOrDefault(e => ChromeBrowserInfo.DriverNameRegex.IsMatch(e.Name)) ?? 
            throw new FileNotFoundException($"Couldn't find chromedriver file in zip {uri}");
        driverEntry.ExtractToFile(filePath, true);
    }

    public async Task<ZipArchive> DownloadZip(Uri uri)
    {
        using var http = new HttpClient();

        await using var apiResponse = await http.GetStreamAsync(uri);
        var zip = new ZipArchive(apiResponse);
        return zip;
    }
}
