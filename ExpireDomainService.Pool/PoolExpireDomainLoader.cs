using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using ExpireDomainService.Common.Utilities;
using ExpireDomainService.Pool.Http;
using ExpireDomainService.Core.Domains;
using ExpireDomainService.Common.Loader;

namespace ExpireDomainService.Pool
{
    public sealed class PoolExpireDomainLoader : ILoader<ExpireDomainName>
    {
        private static readonly string DOWNLOAD_URL = "http://www.pool.com/Downloads/PoolDeletingDomainsList.zip";
        private Temp temp = new Temp();
        private List<string> poolDeletingDomains = new List<string>();
        private volatile int index = 0;

        public PoolExpireDomainLoader(string parameter = "")
        {
            ProcessLoader();
        }

        private static void Download(string downloadUrl, string fileName)
        {
            ZipDownloader downloader = new ZipDownloader();
            downloader.DownloadStart(downloadUrl, fileName);
        }

        private void ProcessLoader()
        {
            using (Temp temp = new Temp())
            {
                string fullName = temp.GetRandomFileName(".zip").FullName;

                Download(DOWNLOAD_URL, fullName);

                ZipHelper.Unzip(fullName, temp.TempDirectory);

                string file = FileHelper.GetFirstFileWithExtension(temp.TempDirectory, ".txt");

                if (!string.IsNullOrEmpty(file))
                {
                    poolDeletingDomains.AddRange(File.ReadAllLines(file));
                }
            }
        }

        public bool HasNext()
        {
            lock (poolDeletingDomains)
            {
                if (index < poolDeletingDomains.Count)
                {
                    return true;
                }

                return false;
            }
        }

        public ExpireDomainName Next()
        {
            lock (poolDeletingDomains)
            {
                string line = poolDeletingDomains[index++];

                string[] contents = line.Split(new char[] { ',' });

                if(contents != null && contents.Length >=2)
                {
                    return new ExpireDomainName(contents[0], contents[1]);
                }

                return null;
            }
        }

        public void Dispose()
        {
            this.poolDeletingDomains.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
