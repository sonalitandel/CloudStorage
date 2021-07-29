using System;
using System.IO;
using Google.Apis.Auth.OAuth2; // Google.Apis.Auth --version 1.30.0
using Google.Cloud.Storage.V1; // Google.Cloud.Storage.V
namespace CloudUploadDownload
{
    public class UploadDownloadFile
    {
        public static StorageClient ConnectionSetUp()
        {
            GoogleCredential credential = null;
            using (var jsonStream = new FileStream("credentials.json", FileMode.Open, //service Key in the form of JSON file
                FileAccess.Read, FileShare.Read))
            {
                credential = GoogleCredential.FromStream(jsonStream); //Loads credential.json file
            }
            var storageClient = StorageClient.Create(credential);
            return storageClient;
        }

        public static string UploadFile(string bucketName, string FileName)
        {
            var storageClientDetail = ConnectionSetUp();
            using (var fileStream = new FileStream(@"C:\All Examples\" + FileName.Trim(), FileMode.Open,
                FileAccess.Read, FileShare.Read)) //File that is supposed to be uploaded
            {
                var result = storageClientDetail.UploadObject(bucketName, "SampleTextFile.txt", "text/plain", fileStream);
                return result.MediaLink;
            }

            //// List objects
            //foreach (var obj in storageClientDetail.ListObjects(bucketName, ""))
            //{
            //    Console.WriteLine(obj.Name);
            //}

        }
        public static void DownloadFile(string bucketName, string FileName)
        {
            var storageClientDetail = ConnectionSetUp();
            // Download file
            using (var fileStream = File.Create("Sample-copy.txt"))
            {
                storageClientDetail.DownloadObject(bucketName, "SampleTextFile.txt", fileStream);
            }
            foreach (var obj in Directory.GetFiles(@"C:\All Examples\downloadFile"))
            {
                Console.WriteLine(obj);
            }

        }
    }
}
