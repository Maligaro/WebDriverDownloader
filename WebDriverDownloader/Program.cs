using CommandLine;
using WebDriverDownloader.Model;
using WebDriverDownloader.Utils;
using WebDriverDownloader.DriverDownloader;

namespace WebDriverDownloader;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var options = Parser.Default.ParseArguments<Options>(args).Value;
        if (options is null)
            return;

        var downloader = new WebDriverDownloaderFacade(Driver.Chromedriver);

        var path = options.Path;

        var version = options.Version is not null ?
            new DriverVersion(options.Version) :
            await downloader.GetBrowserMatchingDriverVersion();

        var platform = options.Platform is not null ?
            PlatformInfo.ParsePlatform(options.Platform) :
            PlatformInfo.GetCurrentPlatform();

        Console.WriteLine(@$"Downloading webdriver for {platform} into {path}");
        await downloader.DownloadDriver(path, version, platform);
        System.Console.WriteLine("Download complete");
    }

    private class Options
    {
        [Option('p', "path", Required = true, HelpText = @"File path for driver download")]
        public string Path { get; set; }

        [Option('v', "version", Required = false, HelpText = @"Version of the web driver to download
If version is not specified downloader will try to detect it from installed browser
You can specity a full version (example ""116.0.5845.96"") or a milestone version (example ""116"")")]
        public string Version { get; set; }

        [Option('o', "platform", Required = false, HelpText = @"Target os for the driver
If os is not specified downloader will use current os")]
        public string Platform { get; set; }
    }
}
