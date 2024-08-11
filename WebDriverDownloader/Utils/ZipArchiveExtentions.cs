using System.IO.Compression;

namespace WebDriverDownloader.Utils;

internal static class ZipArchiveExtentions
{
    public static void CreateDirectoyAndExtractToFile(this ZipArchiveEntry entry, string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        entry.ExtractToFile(filePath, true);
    }
}