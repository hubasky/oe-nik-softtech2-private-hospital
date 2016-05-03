using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HubaskyHospitalManager.Data
{
    /// <summary>
    /// Used to handle basic file operations via ftp connection.
    /// </summary>
    public class FTPConnection
    {

        private string host;
        private string user;
        private string password;
        private string basePath;

        /// <summary>
        /// Creates an instance of FTPConnection class.
        /// </summary>
        /// <param name="host">ftp server host name or IP address (without http:// or ftp:// tags).</param>
        /// <param name="user">Username for the ftp server.</param>
        /// <param name="password">Password for the ftp server.</param>
        /// <param name="basePath">Specify a base path which exists on the server to operate the rest from there.</param>
        public FTPConnection(string host, string user, string password, string basePath)
        {
            this.host = host;
            this.user = user;
            this.password = password;
            this.basePath = basePath;
        }

        /// <summary>
        /// Creates directory on server using ftp connection.
        /// </summary>
        /// <param name="directoryName">Remote path (based on the base path) and name of the directory name on the server.</param>
        public void CreateDirectory(string directoryName)
        {
            string dirname = String.Format("ftp://{0}/{1}/{2}", host, basePath, directoryName);
            FtpWebRequest ftpReq = WebRequest.Create(dirname) as FtpWebRequest;
            ftpReq.Method = WebRequestMethods.Ftp.MakeDirectory;
            ftpReq.Credentials = new NetworkCredential(user, password);
            FtpWebResponse ftpResp = ftpReq.GetResponse() as FtpWebResponse;
            ftpResp.Close();
        }

        /// <summary>
        /// Deletes directory on server using ftp connection.
        /// </summary>
        /// <param name="directoryName">Remote path (based on the base path) and name of the directory on the server.</param>
        public void DeleteDirectory(string directoryName)
        {
            string dirname = String.Format("ftp://{0}/{1}/{2}", host, basePath, directoryName);
            FtpWebRequest ftpReq = WebRequest.Create(dirname) as FtpWebRequest;
            ftpReq.Method = WebRequestMethods.Ftp.RemoveDirectory;
            ftpReq.Credentials = new NetworkCredential(user, password);
            FtpWebResponse ftpResp = ftpReq.GetResponse() as FtpWebResponse;
            ftpResp.Close();
        }

        /// <summary>
        /// Upload a file to a specific server location using ftp connection.
        /// </summary>
        /// <param name="ftpFile">Remote path (based on the base path) and filename on the server.</param>
        /// <param name="localFile">Full path and filename of the file on localhost to be uploaded.</param>
        public void UploadFile(string ftpFile, string localFile)
        {
            using (WebClient client = new WebClient())
            {
                string uploadPath = String.Format("ftp://{0}/{1}/{2}", host, basePath, ftpFile);
                client.Credentials = new NetworkCredential(user, password);
                client.UploadFile(uploadPath, "STOR", localFile);
            }
        }

        /// <summary>
        /// Download a file from remote server using ftp connection.
        /// </summary>
        /// <param name="ftpFile">Remote path (based on the base path) and filename on the server to be downloaded.</param>
        /// <param name="localFile">Full path of the file where to download to.</param>
        public void DownloadFile(string ftpFile, string localFile)
        {
            using (WebClient client = new WebClient())
            {
                string downloadPath = String.Format("ftp://{0}/{1}/{2}", host, basePath, ftpFile);
                client.Credentials = new NetworkCredential(user, password);
                client.DownloadFile(downloadPath, localFile);
            }
        }

        /// <summary>
        /// Delete file on remote server.
        /// </summary>
        /// <param name="ftpFile">Remote path (based on the base path) and filename on the server to be deleted.</param>
        public void DeleteFile(string ftpFile)
        {
            using (WebClient client = new WebClient())
            {
                string filename = String.Format("ftp://{0}/{1}/{2}", host, basePath, ftpFile);
                FtpWebRequest ftpReq = WebRequest.Create(filename) as FtpWebRequest;
                ftpReq.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpReq.Credentials = new NetworkCredential(user, password);
                FtpWebResponse ftpResp = ftpReq.GetResponse() as FtpWebResponse;
                ftpResp.Close();
            }
        }
    }
}
