using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMDO_Launcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //TODO: Another Game EXE Name
            if (!File.Exists(Path.Combine(Application.StartupPath, "[CoolGameName].exe")))
            {
                Application.Run(new InstallerForm1());
            }

            else
            {
                int bytesRead = 0;
                byte[] buffer = new byte[2048];
                //TODO: Updater EXE name
                File.Delete(Path.Combine(Application.StartupPath, "Updater.exe"));

                try
                {
                    //TODO: Updater EXE name and FTP stuff
                    FtpWebRequest updaterequest = CreateFtpWebRequest("ftp://your.domain.com/Updater.exe", "anonymous", "", true);
                    updaterequest.Method = WebRequestMethods.Ftp.DownloadFile;

                    Stream updatereader = updaterequest.GetResponse().GetResponseStream();
                    FileStream updatefileStream = new FileStream(Path.Combine(Application.StartupPath, "PMDO Updater.exe"), FileMode.Create);

                    while (true)
                    {
                        bytesRead = updatereader.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        updatefileStream.Write(buffer, 0, bytesRead);
                    }
                    updatefileStream.Dispose();
                    updatefileStream.Close();
                }

                catch
                {

                }

                if (!Directory.Exists(Path.Combine(Application.StartupPath, "UpdaterCache")))
                {
                    Directory.CreateDirectory(Path.Combine(Application.StartupPath, "UpdaterCache"));
                }

                Process proc = new Process();
                //TODO: Updater EXE name
                proc.StartInfo.FileName = Path.Combine(Application.StartupPath, "Updater.exe");
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();
            }
            
        }

        private static FtpWebRequest CreateFtpWebRequest(string ftpDirectoryPath, string userName, string password, bool keepAlive = false)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpDirectoryPath));

            //Set proxy to null. Under current configuration if this option is not set then the proxy that is used will get an html response from the web content gateway (firewall monitoring system)
            request.Proxy = null;

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = keepAlive;

            request.Credentials = new NetworkCredential(userName, password);

            return request;
        }

    }
}
