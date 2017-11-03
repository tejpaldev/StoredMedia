using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace StoreyedMedia.Infrastructure
{
    public static class S3Cloud
    {

        #region Constants

        static string bucketName = "storied-medias"; 

        #endregion

        public static bool FileUpload(HttpPostedFileBase file,string key)
        {
            Stream localFile = file.InputStream;

            string name = Path.GetFileName(file.FileName);
            string myBucketName = bucketName; //your s3 bucket name   
            string s3DirectoryName = String.Empty;
            string s3FileName = @name; 
            return SendFileToS3(localFile, myBucketName, s3DirectoryName, key);
        }

        private static bool SendFileToS3(System.IO.Stream localFile , string bucketName, string subDirectoryInBucket, string keyInS3)
        {
            bool status = false;
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    InputStream= localFile ,
                    BucketName = bucketName,
                    Key = keyInS3, 
                    ContentType = "text/plain"
                };

                PutObjectResponse response = client.PutObject(putRequest);
                
                status = response.HttpStatusCode.ToString()  == "OK"? true : false;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }

            return status; 
        }

        public static bool IsValidGuid(string str)
        {
            Guid guid;
            return Guid.TryParse(str, out guid);
        }

        public static string GetFileFromS3(string keyInS3)
        {
            String url = string.Empty;
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            try
            {

                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = keyInS3,
                    Expires = DateTime.Now.AddMinutes(15)
                };

                url = client.GetPreSignedURL(request);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }

            return url;
        }


        public static string KeyGenerator()
        { 
            Guid guId;
            guId = Guid.NewGuid();
            return guId.ToString(); 
        }
    }
}
