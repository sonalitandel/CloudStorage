using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Auth.OAuth2; // Google.Apis.Auth --version 1.30.0
using Google.Cloud.Storage.V1; // Google.Cloud.Storage.V

namespace CloudStorageSample
{
    public class FileStorageClass
    {
        public static string downloadFolder = @"C:\All Examples\downloadFile";
        public static StorageClient ConnectionSetUp()
        {
            GoogleCredential credential = null;
            try
            {
                using (var jsonStream = new FileStream("credentials.json", FileMode.Open, //service Key in the form of JSON file
                    FileAccess.Read, FileShare.Read))
                {
                    credential = GoogleCredential.FromStream(jsonStream); //Loads credential.json file
                }
                var storageClient = StorageClient.Create(credential);
                return storageClient;
            }
            catch(FileNotFoundException FileEx)
            {
                throw FileEx;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string UploadFile(string bucketName, string FileName)
        {
            try
            {
                var storageClientDetail = ConnectionSetUp();
                using (var fileStream = new FileStream(@"C:\All Examples\" + FileName.Trim(), FileMode.Open,
                    FileAccess.Read, FileShare.Read)) //File that is supposed to be uploaded
                {
                    var result = storageClientDetail.UploadObject(bucketName, FileName.Trim(), "text/plain", fileStream);
                    return result.MediaLink;
                }
            }
            catch (FileNotFoundException FileEx)
            {
                throw FileEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }
        public static string DownloadFile(string bucketName, string FileName)
        {
            var storageClientDetail = ConnectionSetUp();
            // Download file
            try
            {
                using (var fileStream = File.Create(Path.Combine(downloadFolder, FileName)))
                {
                    storageClientDetail.DownloadObject(bucketName, "Sonali.txt", fileStream);
                }   
                    if (Directory.GetFiles(downloadFolder).Any())
                    {
                        if (File.Exists(Path.Combine(downloadFolder, FileName)))
                            return "File Has been downloaded to the folder";
                        else
                            return "Failed to download file";
                    }
            }
            catch (FileNotFoundException FileEx)
            {
                throw FileEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
