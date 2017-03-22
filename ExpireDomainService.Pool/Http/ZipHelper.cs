using System.IO.Compression;

namespace ExpireDomainService.Pool.Http
{
    public static class ZipHelper
    {
        public static void Unzip(string zipPath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipPath, extractPath);
        }
    }
}
