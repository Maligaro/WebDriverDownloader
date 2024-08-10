namespace WebDriverDownloader.Services;

internal interface IDriverDownloader
{
    Task DownloadDriver(Uri uri, string filePath);
}
