using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Common.Utilities
{
    public class Temp : IDisposable
    {
        private readonly string tempDirectory;

        public string TempDirectory
        {
            get
            {
                return tempDirectory;
            }
        }

        public Temp()
        {
            tempDirectory = Path.GetTempPath() + Path.GetRandomFileName();

            if (!Directory.Exists(tempDirectory))
            {
                Directory.CreateDirectory(tempDirectory);
            }
        }


        public FileInfo GetRandomFileName()
        {
            return new FileInfo(tempDirectory + Path.DirectorySeparatorChar + Path.GetRandomFileName());
        }

        public FileInfo GetRandomFileName(string ext)
        {
            return new FileInfo(tempDirectory + Path.DirectorySeparatorChar + Path.GetRandomFileName() + ext);
        }


        private void Clean()
        {
            try
            {
                Logger.Instance.Info("Deleting: " + tempDirectory);
                Directory.Delete(tempDirectory, true);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex.ToString());
            }
        }

        public void Dispose()
        {
            Clean();
            GC.SuppressFinalize(this);
        }
    }
}