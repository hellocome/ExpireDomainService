using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using ExpireDomainService.Common.Utilities;

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
