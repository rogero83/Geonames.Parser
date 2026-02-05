using System.IO;
using System.IO.Compression;
using System.Text;

namespace Geonames.Parser.Tests.Utility;

public static class ZipUtility
{
    public static MemoryStream CreateZipWithContent(string isoCode, string content)
    {
        var ms = new MemoryStream();
        using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry($"{isoCode}.txt");
            using var entryStream = entry.Open();
            using var sw = new StreamWriter(entryStream, Encoding.UTF8);
            sw.Write(content);
        }
        ms.Position = 0;
        return ms;
    }
}
