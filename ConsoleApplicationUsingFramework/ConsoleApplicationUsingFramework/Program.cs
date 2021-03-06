using System;
using System.IO;
using System.Runtime.InteropServices;
using Google.Apis.Auth.OAuth2; // Google.Apis.Auth --version 1.30.0
using Google.Cloud.Storage.V1; // Google.Cloud.Storage.V1
using CloudStorageSample;
namespace ConsoleApplicationUsingFramework
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //Code reference  https://gist.github.com/henrikj242/af06ac41fc9554dab387c0bb3a994f85
            string message = "Dot Net example: Upload file to Google Cloud Bucket. And also download";
           
            string bucketName = "bucket"; //giving the bucket name

            //Uploading the file
            Console.WriteLine("Upload a file?");
            string UpldFileResp = Console.ReadLine();
            if (UpldFileResp.ToLower() == "yes")
            {
                Console.WriteLine("Enter the filename to be archived");
                string fileName = Console.ReadLine();
                string Result = FileStorageClass.UploadFile(bucketName, fileName);
                Console.WriteLine(Result);
            }
            ////Downloading the file
            Console.WriteLine("Download a file?");
            string DwnldFileResp = Console.ReadLine();
            if(DwnldFileResp.ToLower()=="yes")
            {
                Console.WriteLine("Enter the filename to be retrived");
                string DownloadfileName = Console.ReadLine();
                FileStorageClass.DownloadFile(bucketName, DownloadfileName);

            }

            //Upload a folder
            Console.WriteLine("Upload a Folder with file?");
            string UpldFldrResp = Console.ReadLine();
            if (UpldFldrResp.ToLower() == "yes")
            {
                Console.WriteLine("Enter the foldername to be created");
                string UploadfolderName = Console.ReadLine();
                Console.WriteLine("Enter the filename to be uploaded");
                string UploadFileName = Console.ReadLine();
                var res = FileStorageClass.AddFolder(bucketName, UploadfolderName, UploadFileName);
            }

            Console.WriteLine($"Platform: .NET Core 2.0");
            Console.WriteLine($"OS: {RuntimeInformation.OSDescription}");
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
