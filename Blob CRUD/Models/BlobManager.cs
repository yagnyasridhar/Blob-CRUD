using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Blob_CRUD.Models
{
    public class BlobManager
    {
        CloudBlobClient blobClient = null;
        CloudBlobContainer blobContainer = null;

        public BlobManager()
        {
            try
            {
                string connectionString = "";
                CloudStorageAccount cloudStorage = CloudStorageAccount.Parse(connectionString);
                blobClient = cloudStorage.CreateCloudBlobClient();
                blobContainer = blobClient.GetContainerReference("image123");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UploadFile(HttpPostedFileBase httpPostedFile)
        {
            string uri = string.Empty;

            if (httpPostedFile == null || httpPostedFile.ContentLength == 0)
                return null;

            try
            {
                string fileName = httpPostedFile.FileName;
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.Properties.ContentType = httpPostedFile.ContentType;
                blockBlob.UploadFromStream(httpPostedFile.InputStream);
                uri = blockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return uri;
        }

        public List<string> GetAll()
        {
            List<string> fileCollection = new List<string>();
            try
            {
                foreach (var blob in blobContainer.ListBlobs())
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blockBlob = (CloudBlockBlob)blob;
                        fileCollection.Add(blockBlob.Uri.AbsoluteUri);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileCollection;
        }

        public bool Delete(string uri)
        {
            bool status = false;
            try
            {
                Uri uriObj = new Uri(uri);
                string BlobName = Path.GetFileName(uriObj.LocalPath);
                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(BlobName);
                status = blockBlob.DeleteIfExists();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return status;
        }
    }
}