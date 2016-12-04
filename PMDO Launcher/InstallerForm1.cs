// Copyright (C) 2016 JordantheBuizel, PMDO Staff

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.
// <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using IWshRuntimeLibrary;
using System.Diagnostics;

namespace PMDO_Launcher
{
    public partial class InstallerForm1 : Form
    {
        Stopwatch SecondTimer = new Stopwatch();
        string InstallLocation = null;

        public InstallerForm1()
        {
            InitializeComponent();

            this.btnNext2.Click += new EventHandler(btnNext2_Click);
            this.btnBrowse.Click += new EventHandler(btnBrowse_Click);
            this.btnFinish.Click += new EventHandler(btnFinish_Click);

            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            //TODO: Install Folder Name Needed
            txtInstallLoc.Text = Path.Combine(Application.StartupPath.Split('\\')[0] + "\\", "Program Files", "[GameInstallFolder]");

            lblLoading.Location = new Point(progressBar1.Location.X, lblLoading.Location.Y);

            progressBar1.Visible = false;
            
        }

        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNext1_Click(object sender, EventArgs e)
        {

            rtbTerms.Visible = false;
            btnNext1.Visible = false;
            btnNext2.Visible = true;
            lblSelect.Visible = true;
            txtInstallLoc.Visible = true;
            btnBrowse.Visible = true;



        }

        private void btnNext2_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists(txtInstallLoc.Text))
            {
                Directory.CreateDirectory(txtInstallLoc.Text);
            }

            InstallLocation = txtInstallLoc.Text;


            btnNext2.Visible = false;
            lblSelect.Visible = false;
            txtInstallLoc.Visible = false;
            btnBrowse.Visible = false;
            progressBar1.Visible = true;
            pbLoading.Visible = true;
            lblLoading.Visible = true;

            backgroundWorker1.RunWorkerAsync();
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbLoading.Visible = false;
            lblLoading.Visible = false;
            btnFinish.Visible = true;
            btnCancel.Visible = false;
            chbxLaunch.Visible = true;
            chbxShortcut.Visible = true;
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (chbxShortcut.Checked)
            {
                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                //TODO: Game Name Here
                string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\[GameName].lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                //TODO: Shortcut Description
                shortcut.Description = "BuizelsRCoolerThanYou Shortcut";
                shortcut.Hotkey = "Ctrl+Shift+N";
                //TODO: Add the name you decide for Launcher
                shortcut.TargetPath = Path.Combine(InstallLocation, "[CoolName] Launcher.exe");
                shortcut.Save();
            }

            if (chbxLaunch.Checked)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = Path.Combine(InstallLocation, "[CoolName] Launcher.exe");
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();
            }
            
            if (!Directory.Exists(Path.Combine(InstallLocation, "UpdaterCache")))
            {
                Directory.CreateDirectory(Path.Combine(InstallLocation, "UpdaterCache"));
            }

            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog installFolderPicker = new FolderBrowserDialog();
            DialogResult result = installFolderPicker.ShowDialog();

            if (!string.IsNullOrWhiteSpace(installFolderPicker.SelectedPath))
            {
                //TODO: Install Folder Name Needed
                if (!installFolderPicker.SelectedPath.Contains("[InstallName]"))
                    txtInstallLoc.Text = Path.Combine(installFolderPicker.SelectedPath, "[InstallName]");

                else
                    txtInstallLoc.Text = installFolderPicker.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel the installation?", "Cancel Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private FtpWebRequest CreateFtpWebRequest(string ftpDirectoryPath, string userName, string password, bool keepAlive = false)
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

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[1024 * 1024];


            //TODO: Input FTP stuff here
            FtpWebRequest request = CreateFtpWebRequest("ftp://your.domain.com/Release.rar", "anonymous", "", true);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            Stream reader = request.GetResponse().GetResponseStream();
            FileStream fileStream = new FileStream(Path.Combine(InstallLocation, "Release.zip"), FileMode.Create);

            
            SecondTimer.Start();
            long totalbytes = 0;
            while (true)
            {
                bytesRead = reader.Read(buffer, 0, buffer.Length);

                totalbytes += bytesRead;
                double percentage = totalbytes * 100.0 / 153815951;

                progressBar1.Value = (int)percentage;
                progressBar1.Invalidate();

                lblLoading.Text = "Downloading " + percentage + "%...";


                if (bytesRead == 0)
                    break;

                fileStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Dispose();
            fileStream.Close();

            lblLoading.Text = "Installing...";

            System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(InstallLocation, "Release.zip"), InstallLocation);


        }

    }
}
