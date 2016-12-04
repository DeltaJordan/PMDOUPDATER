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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PMDO_Updater
{
    public partial class MainUpdaterForm : Form
    {

        #region button1Constructors

        public List<int> missingRevisions = new List<int>();
        public bool revisionsXMLLatest = false;
        public int latestUpdateRevision = 0;
        public List<string> missingRevisionsStrings = new List<string>();
        public List<string> revisionBuilder = new List<string>();
        public List<string> updateTypes = new List<string>();
        public List<string> updateNotes = new List<string>();
        public List<string> installedRevisionsList = new List<string>();

        #endregion

        public MainUpdaterForm()
        {
            InitializeComponent();

            backgroundWorker1.RunWorkerAsync();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void UpdatePMDO()
        {
            //Checks if the game has been run before
            bool isnotFirstTimeInstall = File.Exists(Path.Combine(Application.StartupPath, "InstalledUpdates.xml"));
            int bytesRead = 0;
            byte[] buffer = new byte[2048];

            //Finds the missing revisions
            if (isnotFirstTimeInstall)
            {
                XmlDocument installedUpdates = new XmlDocument();
                installedUpdates.Load(Path.Combine(Application.StartupPath, "InstalledUpdates.xml"));
                XmlNode installedRevisions = installedUpdates.DocumentElement["InstalledRevisions"];

                installedRevisionsList = installedRevisions.InnerText.Split(",".ToCharArray()).ToList();

                //test
                for (int i = 0; i < installedRevisionsList.Count; i++)
                {
                    int revisionNum = Int32.Parse(installedRevisionsList[i]);
                    if (i + 1 < installedRevisionsList.Count)
                    {
                        if (revisionNum + 1 != Int32.Parse(installedRevisionsList[i + 1]))
                        {
                            missingRevisions.Add(revisionNum + 1);
                        }
                    }
                    else
                    {
                        missingRevisions.Add(revisionNum + 1);
                    }
                }

                for (int i = 0; i < missingRevisions.Count; i++)
                {
                    try
                    {
                        //TODO: Input FTP stuff here
                        FtpWebRequest request = CreateFtpWebRequest("ftp://your.domain.com/UpdateData" + missingRevisions[i] + ".xml", "anonymous", "", true);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        Stream reader = request.GetResponse().GetResponseStream();
                        FileStream fileStream = new FileStream(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateData" + missingRevisions[i].ToString() + ".xml"), FileMode.Create);

                        while (true)
                        {
                            bytesRead = reader.Read(buffer, 0, buffer.Length);

                            if (bytesRead == 0)
                                break;

                            fileStream.Write(buffer, 0, bytesRead);
                        }
                        fileStream.Dispose();
                        fileStream.Close();
                    }
                    catch
                    {
                        if (missingRevisions.Count == 1)
                        {
                            Process.Start(Path.Combine(Application.StartupPath, "Pokemon Mystery Dungeon Online.exe"));
                            Close();
                        }
                    }
                }

                //Downloads all the update data
                while (true)
                {
                    try
                    {
                        int i = missingRevisions[missingRevisions.Count - 1];
                        i++;
                        //TODO: Input FTP stuff here
                        FtpWebRequest request = CreateFtpWebRequest("ftp://your.domain.com/UpdateData" + i + ".xml", "anonymous", "", true);
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        Stream reader = request.GetResponse().GetResponseStream();
                        FileStream fileStream = new FileStream(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateData" + i + ".xml"), FileMode.Create);

                        while (true)
                        {
                            bytesRead = reader.Read(buffer, 0, buffer.Length);

                            if (bytesRead == 0)
                                break;

                            fileStream.Write(buffer, 0, bytesRead);
                        }
                        fileStream.Dispose();
                        fileStream.Close();

                        missingRevisions.Add(i);
                    }
                    catch
                    {
                        //If theirs an error we assume that this revision has not been pushed yet
                        break;
                    }
                }

            }

            //Looks like this is your first time. 
            else
            {
                //TODO: Input FTP stuff here
                FtpWebRequest request = CreateFtpWebRequest("ftp://your.domain.com/UpdateData.xml", "anonymous", "", true);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                Stream reader = request.GetResponse().GetResponseStream();
                FileStream fileStream = new FileStream(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateData.xml"), FileMode.Create);

                while (true)
                {
                    bytesRead = reader.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                        break;

                    fileStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Dispose();
                fileStream.Close();
            }

            XmlDocument newSettingsXml = new XmlDocument();

            //Gets all the info we need to install the updates
            if (isnotFirstTimeInstall)
            {

                for (int i = 0; i < missingRevisions.Count; i++)
                {
                    newSettingsXml.Load(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateData" + missingRevisions[i].ToString() + ".xml"));

                    XmlNode newRevision = newSettingsXml.DocumentElement["Revision"];
                    XmlNode fileName = newSettingsXml.DocumentElement["fileName"];
                    XmlNode ziporexe = newSettingsXml.DocumentElement["SimplePacked"];
                    XmlNode changelog = newSettingsXml.DocumentElement["UpdateNotes"];

                    //don't remember why I did this :?
                    latestUpdateRevision = Int32.Parse(newRevision.InnerText);
                    //Gets if the update is a zip, main game exe, or rar(soon)
                    updateTypes.Add(ziporexe.InnerText);
                    //Puts all the changes into one nice long List<string>
                    updateNotes.Add(changelog.InnerText);
                }
            }

            //This is just for proof of concept; the 1st update will always be a zip
            else
            {
                newSettingsXml.Load(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateData.xml"));

                XmlNode newRevision = newSettingsXml.DocumentElement["Revision"];
                XmlNode fileName = newSettingsXml.DocumentElement["fileName"];
                XmlNode ziporexe = newSettingsXml.DocumentElement["SimplePacked"];
                XmlNode changelog = newSettingsXml.DocumentElement["UpdateNotes"];

                latestUpdateRevision = Int32.Parse(newRevision.InnerText);
                updateTypes.Add(ziporexe.InnerText);
                updateNotes.Add(changelog.InnerText);
            }
            

            //Time to download all those updates ;D
            for (int i = 0; i < updateTypes.Count; i++)
            {
                if (updateTypes[i] == "zip")
                {
                    int a = i;
                    if (isnotFirstTimeInstall)
                    {
                        try
                        {
                            //TODO: Input FTP stuff here
                            FtpWebRequest updaterequest = CreateFtpWebRequest("ftp://your.domain.com/update" + missingRevisions[i].ToString() + ".zip", "anonymous", "", true);
                            updaterequest.Method = WebRequestMethods.Ftp.DownloadFile;

                            Stream updatereader = updaterequest.GetResponse().GetResponseStream();
                            FileStream updatefileStream = new FileStream(Path.Combine(Application.StartupPath, "UpdaterCache", "update" + missingRevisions[a].ToString() + ".zip"), FileMode.Create);

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

                        if (!Directory.Exists(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateRevision" + missingRevisions[a].ToString())))
                        {
                            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateRevision" + missingRevisions[a].ToString()));
                        }
                        System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Application.StartupPath, "UpdaterCache", "update" + missingRevisions[a].ToString() + ".zip"), Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateRevision" + missingRevisions[a].ToString()));
                        if (a != missingRevisions.Count - 1)
                        {
                            File.Delete(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateRevision" + missingRevisions[a].ToString(), "Release", "Pokemon Mystery Dungeon Online.exe"));
                        }
                        CopyAll(new DirectoryInfo(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateRevision" + missingRevisions[a].ToString(), "Release")), new DirectoryInfo(Application.StartupPath));

                        Directory.Delete(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateRevision" + missingRevisions[a].ToString()), true);
                        File.Delete(Path.Combine(Application.StartupPath, "UpdaterCache", "update" + missingRevisions[a].ToString() + ".zip"));

                        missingRevisionsStrings.Add(missingRevisions[a].ToString());
                    }


                    else
                    {

                        /*File.Delete(Path.Combine(Application.StartupPath, "UpdateData.xml"));
                        File.Move(Path.Combine(Application.StartupPath, "UpdaterCache", "UpdateData.xml"), Path.Combine(Application.StartupPath, "UpdateData.xml"));*/

                        //TODO: Input FTP stuff here
                        FtpWebRequest updaterequest = CreateFtpWebRequest("ftp://your.domain.com/update.zip", "anonymous", "", true);
                        updaterequest.Method = WebRequestMethods.Ftp.DownloadFile;

                        Stream updatereader = updaterequest.GetResponse().GetResponseStream();
                        FileStream updatefileStream = new FileStream(Path.Combine(Application.StartupPath, "UpdaterCache", "update.zip"), FileMode.Create);

                        while (true)
                        {
                            bytesRead = updatereader.Read(buffer, 0, buffer.Length);

                            if (bytesRead == 0)
                                break;

                            updatefileStream.Write(buffer, 0, bytesRead);
                        }
                        updatefileStream.Dispose();
                        updatefileStream.Close();

                        var dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, "UpdaterCache"));
                        dirInfo.Attributes &= ~FileAttributes.ReadOnly;
                        System.IO.Compression.ZipFile.ExtractToDirectory(Path.Combine(Application.StartupPath, "UpdaterCache", "update.zip"), Path.Combine(Application.StartupPath, "UpdaterCache"));

                        CopyAll(new DirectoryInfo(Path.Combine(Application.StartupPath, "UpdaterCache", "Release")), new DirectoryInfo(Application.StartupPath));

                        Directory.Delete(Path.Combine(Application.StartupPath, "UpdaterCache", "Release"), true);
                        File.Delete(Path.Combine(Application.StartupPath, "UpdaterCache", "update.zip"));

                        XmlTextWriter writer = new XmlTextWriter(Path.Combine(Application.StartupPath, "InstalledUpdates.xml"), System.Text.Encoding.UTF8);
                        writer.WriteStartDocument(true);
                        writer.Formatting = Formatting.Indented;
                        writer.Indentation = 2;
                        writer.WriteStartElement("InstalledUpdates");
                        writer.WriteStartElement("InstalledRevisions");
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();

                        XmlDocument settingsXml = new XmlDocument();
                        settingsXml.Load(Path.Combine(Application.StartupPath, "InstalledUpdates.xml"));
                        XmlNode revision = settingsXml.DocumentElement["InstalledRevisions"];

                        revision.InnerText = "1";
                        settingsXml.Save(Path.Combine(Application.StartupPath, "InstalledUpdates.xml"));

                        //Run the launcher, so the updater will install post-inital updates
                        //TODO: Add the name you decide for Launcher
                        Process.Start(Path.Combine(Application.StartupPath, "[CoolName] Launcher.exe"));
                        Close();
                        break;
                    }
                }

                //Sometime soon
                else if (updateTypes[i] == "rar")
                {

                    //Code HERE
                    
                }

                else
                {
                    //TODO: Input FTP stuff here and Game Exe File name
                    FtpWebRequest updaterequest = CreateFtpWebRequest("ftp://your.domain.com/YourGameMain.exe", "anonymous", "", true);
                    updaterequest.Method = WebRequestMethods.Ftp.DownloadFile;

                    Stream updatereader = updaterequest.GetResponse().GetResponseStream();
                    FileStream updatefileStream = new FileStream(Path.Combine(Application.StartupPath, "Pokemon Mystery Dungeon Online.exe"), FileMode.Create);

                    while (true)
                    {
                        bytesRead = updatereader.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        updatefileStream.Write(buffer, 0, bytesRead);
                    }
                    updatefileStream.Dispose();
                    updatefileStream.Close();

                    missingRevisionsStrings.Add(missingRevisions[i].ToString());

                }

            }

            CreateRevisionXML();
            MessageBox.Show("Changelog: " + String.Join(" ", updateNotes), "PMDO Update Finished!");
            Process.Start(Path.Combine(Application.StartupPath, "Pokemon Mystery Dungeon Online.exe"));
            Close();

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



        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(UpdatePMDO));
            t.SetApartmentState(System.Threading.ApartmentState.STA); //Set the thread to STA
            t.Start();
        }

        private void CreateRevisionXML()
        {
            XmlDocument settingsXml = new XmlDocument();
            settingsXml.Load(Path.Combine(Application.StartupPath, "InstalledUpdates.xml"));
            XmlNode revision = settingsXml.DocumentElement["InstalledRevisions"];


            revisionBuilder.Add(revision.InnerText);
            revisionBuilder.AddRange(missingRevisionsStrings);
            revision.InnerText = String.Join(",", revisionBuilder);
            settingsXml.Save(Path.Combine(Application.StartupPath, "InstalledUpdates.xml"));
        }
    }
}
