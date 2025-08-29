using Azure.Storage.Blobs;

namespace ABCretailpoe1.Services
{
    public class BlobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorage(string connectionString, string v)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerName = "productimages"; 
        }

        public async Task<string> UploadAsync(string fileName, Stream fileStream)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream);
            return blobClient.Uri.ToString();
        }

        public async Task DeleteBlobAsync(string blobUri)
        {
            Uri uri = new Uri(blobUri);
            //extreacts the blob name from that url by taking the last segment of the patch 
            string blobName = uri.Segments[^1];
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync(Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);
        }
    }
}
