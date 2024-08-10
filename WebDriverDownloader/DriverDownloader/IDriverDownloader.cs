namespace WebDriverDownloader.DriverDownloader;

internal interface IDriverDownloader
{
    Task DownloadDriver(Uri uri, string filePath);
}
