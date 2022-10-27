using API_Gateway.Common.Logics.Interface;
using API_Gateway.Common.Models;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway.Common.Logics
{
    public class FileManagerLogic : IFileManagerLogic
    {
        private readonly BlobServiceClient _blobServiceCLient;

        public FileManagerLogic(BlobServiceClient blobServiceClient)
        {
            _blobServiceCLient = blobServiceClient;
        }

        public async Task Upload(FileModel model)
        {
            var blobContainer = _blobServiceCLient.GetBlobContainerClient("apim-errors");

            var blobClient = blobContainer.GetBlobClient(model.MyFile.FileName);

            await blobClient.UploadAsync(model.MyFile.OpenReadStream());
        }

        public async Task UploadBlob(string fileName, string error)
        {
            var blobContainer = _blobServiceCLient.GetBlobContainerClient("apim-errors");

            using (var memoryStream = new MemoryStream())
            {
                var writer = new StreamWriter(memoryStream);

                writer.Write(error);
                writer.Flush();
                memoryStream.Position = 0;

                var blobClient = blobContainer.GetBlobClient(fileName);

                await blobClient.UploadAsync(memoryStream, true);
                writer.Close();
            }
        }

        public async Task MemberUpdateUploadBlob(string fileName, string result)
        {
            var blobContainer = _blobServiceCLient.GetBlobContainerClient("apim-member-updates");

            using (var memoryStream = new MemoryStream())
            {
                var writer = new StreamWriter(memoryStream);

                writer.Write(result);
                writer.Flush();
                memoryStream.Position = 0;

                var blobClient = blobContainer.GetBlobClient(fileName);

                await blobClient.UploadAsync(memoryStream, true);
                writer.Close();
            }
        }

        public async Task ActivityLogUploadBlob(string fileName, string result)
        {
            var blobContainer = _blobServiceCLient.GetBlobContainerClient("activity-log");

            using (var memoryStream = new MemoryStream())
            {
                var writer = new StreamWriter(memoryStream);

                writer.Write(result);
                writer.Flush();
                memoryStream.Position = 0;

                var blobClient = blobContainer.GetBlobClient(fileName);

                await blobClient.UploadAsync(memoryStream, true);
                writer.Close();
            }
        }

    }
}
