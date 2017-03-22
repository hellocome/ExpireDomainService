using System;
using System.Threading;
using System.Net;

using ExpireDomainService.Common.Logging;

namespace ExpireDomainService.Pool.Http
{
    public sealed class ZipDownloader
    {
        private WebClient wc = new WebClient();
        private static object lockObj = new object();   
        private static EventWaitHandle wait = new EventWaitHandle(false, EventResetMode.ManualReset);

        public bool DownloadStart(string url, string saveTo)
        {
            try
            {
                Logger.Instance.Info("Starting Download..");

                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);

                wait.Reset();
                wc.DownloadFileAsync(new Uri(url), saveTo);
                wait.WaitOne();

                Logger.Instance.Info("Download Successful!");

                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    if (System.IO.File.Exists(saveTo))
                    {
                        System.IO.File.Delete(saveTo);
                    }
                }
                catch
                {
                    Logger.Instance.Error("Delete File Failed!");
                }

                Logger.Instance.Error("Download Failed!", ex);
                return false;
            }
        }


        void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Logger.Instance.Info(string.Format("{0}", e.Cancelled ? "Cancelled" : "Done"));
            }
            else
            {
                Logger.Instance.Info(string.Format("Error"));
            }

            wait.Set();
        }

        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage % 20 == 0)
            {
                Logger.Instance.Debug(string.Format("{0}%", e.ProgressPercentage));
            }
        }
    }
}
