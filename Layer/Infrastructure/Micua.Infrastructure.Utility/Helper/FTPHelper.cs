using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Micua.Infrastructure.Utility
{
    /// <summary>
    /// Ftp帮助类
    /// </summary>
    public class FtpHelper
    {
        #region 字段
        string ftpUrl;
        string ftpUsername;
        string ftpPassword;
        string ftpServer;
        string ftpRemotePath;
        #endregion

        /// <summary>  
        /// 连接Ftp服务器
        /// </summary>  
        /// <param name="ftpServer">Ftp连接地址</param>  
        /// <param name="ftpRemotePath">指定Ftp连接成功后的当前目录, 如果不指定即默认为根目录</param>  
        /// <param name="ftpUsername">用户名</param>  
        /// <param name="ftpPassword">密码</param>  
        public FtpHelper(string ftpServer, string ftpRemotePath, string ftpUsername, string ftpPassword)
        {
            this.ftpServer = ftpServer;
            this.ftpRemotePath = ftpRemotePath;
            this.ftpUsername = ftpUsername;
            this.ftpPassword = ftpPassword;
            this.ftpUrl = "ftp://" + ftpServer + "/" + ftpRemotePath + "/";
        }

        /// <summary>  
        /// 上传  
        /// </summary>   
        public void Upload(string fileName)
        {
            FileInfo fileInf = new FileInfo(fileName);
            FtpWebRequest reqFtp;
            reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + fileInf.Name));
            reqFtp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.KeepAlive = false;
            reqFtp.UseBinary = true;
            reqFtp.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFtp.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 下载  
        /// </summary>   
        public void Download(string filePath, string fileName)
        {
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                FtpWebRequest reqFtp;
                reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + fileName));
                reqFtp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                reqFtp.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFtp.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 删除文件  
        /// </summary>  
        public void Delete(string fileName)
        {
            try
            {
                FtpWebRequest reqFtp;
                reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + fileName));
                reqFtp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                reqFtp.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFtp.KeepAlive = false;
                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 获取当前目录下明细(包含文件和文件夹)  
        /// </summary>  
        public IList<string> GetFilesDetailList()
        {
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl));
                ftp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>  
        /// 获取Ftp文件列表(包括文件夹)
        /// </summary>   
        private IList<string> GetAllList(string url)
        {
            List<string> list = new List<string>();
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(new Uri(url));
            req.Credentials = new NetworkCredential(ftpPassword, ftpPassword);
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            req.UseBinary = true;
            req.UsePassive = true;
            try
            {
                using (FtpWebResponse res = (FtpWebResponse)req.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            list.Add(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return list;
        }

        /// <summary>  
        /// 获取当前目录下文件列表(不包括文件夹)  
        /// </summary>  
        public IList<string> GetFileList(string url)
        {
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFtp;
            try
            {
                reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(ftpPassword, ftpPassword);
                reqFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFtp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {

                    if (line.IndexOf("<DIR>") == -1)
                    {
                        result.Append(Regex.Match(line, @"[\S]+ [\S]+", RegexOptions.IgnoreCase).Value.Split(' ')[1]);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result.ToString().Split('\n');
        }

        /// <summary>  
        /// 判断当前目录下指定的文件是否存在  
        /// </summary>  
        /// <param name="RemoteFileName">远程文件名</param>  
        public bool FileExist(string RemoteFileName)
        {
            var fileList = GetFileList("*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>  
        /// 创建文件夹  
        /// </summary>   
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFtp;
            reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + dirName));
            reqFtp.Method = WebRequestMethods.Ftp.MakeDirectory;
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            ftpStream.Close();
            response.Close();
        }

        /// <summary>  
        /// 获取指定文件大小  
        /// </summary>  
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFtp;
            long fileSize = 0;
            reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + filename));
            reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            fileSize = response.ContentLength;
            ftpStream.Close();
            response.Close();
            return fileSize;
        }

        /// <summary>  
        /// 更改文件名  
        /// </summary> 
        public void ReName(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFtp;
            reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl + currentFilename));
            reqFtp.Method = WebRequestMethods.Ftp.Rename;
            reqFtp.RenameTo = newFilename;
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            FtpWebResponse response = (FtpWebResponse)reqFtp.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            ftpStream.Close();
            response.Close();
        }

        /// <summary>  
        /// 移动文件  
        /// </summary>  
        public void MovieFile(string currentFilename, string newDirectory)
        {
            ReName(currentFilename, newDirectory);
        }

        /// <summary>  
        /// 切换当前目录  
        /// </summary>  
        /// <param name="isRoot">true:绝对路径 false:相对路径</param>   
        public void GotoDirectory(string directoryName, bool isRoot)
        {
            if (isRoot)
            {
                ftpRemotePath = directoryName;
            }
            else
            {
                ftpRemotePath += directoryName + "/";
            }
            ftpUrl = "ftp://" + ftpServer + "/" + ftpRemotePath + "/";
        }
    }
}