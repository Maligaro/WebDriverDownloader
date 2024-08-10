using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WebDriverDownloader.DriverDownloader;
using WebDriverDownloader.Model;
using WebDriverDownloader.Utils;

namespace WebDriverDownloader.Chrome;

internal class ChromeBrowserVersionDetector : IBrowserVersionDetector
{

    public async Task<BrowserVersion> GetInstalledBrowserVersion()
    {
        var platform = PlatformInfo.GetCurrentPlatform();
        var architecture = ArchitectureInfo.GetCurrentArchitecture();
        Assert.SystemIsSupported(Driver.Chromedriver, platform, architecture, ChromeBrowserInfo.SupportedPlatforms);

        string chromeVersion;
        if (platform == OSPlatform.Windows)
        {
            chromeVersion = GetChromeVersionFromRegistry();
        }
        else if (platform == OSPlatform.Linux || platform == OSPlatform.OSX)
        {
            chromeVersion = await GetChromeVersionFromTerminal();
        }
        else
            throw new NotImplementedException();

        return new BrowserVersion(chromeVersion);
    }

    private const string _windowsRegistryChromePathForCurrentUser = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";
    private const string _windowsRegistryChromePathFofAllUsers = @"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe";
    private string GetChromeVersionFromRegistry()
    {
        var pathEntry = Registry.GetValue(_windowsRegistryChromePathForCurrentUser, "", null) ??
            Registry.GetValue(_windowsRegistryChromePathFofAllUsers, "", null);

        if (pathEntry is null)
            throw new Exception($"Can't find Chrome browser path in registry entries \"{_windowsRegistryChromePathForCurrentUser}\", \"{_windowsRegistryChromePathFofAllUsers}\", ensure Google Chrome is installed");

        var path = pathEntry.ToString();
        var chromeVersion = FileVersionInfo.GetVersionInfo(path).FileVersion;
        return chromeVersion;
    }

    private async Task<string> GetChromeVersionFromTerminal()
    {
        var process = Process.Start("google-chrome", "--version");
        await process.WaitForExitAsync();
        var versionInfo = await process.StandardOutput.ReadToEndAsync();
        var fullVersion = ChromeBrowserInfo.VersionRegex.Match(versionInfo).Groups["value"].Value;
        return fullVersion;
    }
}
