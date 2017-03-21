using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExpireDomainService.Common.Utilities
{
    public static class FileHelper
    {
        public static string GetFirstFileWithExtension(string dir, string ext)
        {
            FileInfo[] files = new DirectoryInfo(dir).GetFiles(string.Format("*{0}", ext));

            if(files != null && files.Length > 0)
            {
                return files[0].FullName;
            }

            return string.Empty;
        }
    }
}
