using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;

public class Uploader : MonoBehaviour
{
	public string FTPHost;
	public string FTPUserName;
	public string FTPPassword;
    public string FTPPath;
    public bool isEnable;
    private string FilePath;

	public IEnumerator UploadImage(string filename)
	{
        yield return new WaitForSeconds(1.0f);
        using (System.Net.WebClient client = new System.Net.WebClient())
		{ 
            string ftppath = "ftp://" + FTPHost + "/" + FTPPath + "/" + filename;
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Screenshots/" + filename;
            if (isEnable)
            {
                client.Credentials = new System.Net.NetworkCredential(FTPUserName, FTPPassword);
                client.UploadFile(ftppath, "STOR", @FilePath);
            }
        }
    }

    public void UploadImage2(string filename)
    {
        using (System.Net.WebClient client = new System.Net.WebClient())
        {
            string ftppath = "ftp://" + FTPHost + "/" + FTPPath + "/" + filename;
            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Screenshots/" + filename;
            if(isEnable)
            {
                client.Credentials = new System.Net.NetworkCredential(FTPUserName, FTPPassword);
                client.UploadFile(ftppath, "STOR", @FilePath);
            }
        }
    }
}