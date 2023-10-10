using System.Reflection.Metadata.Ecma335;
using Dynamo.IO.S3.Models;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace Dynamo.IO.S3.Services
{
    public class StorageService : IStorageService
    {
        public async Task<S3ResponseDto> UploadFileAsync(S3Object obj)
        {
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            };

            var response = new S3ResponseDto();
            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = obj.InputStream,
                    Key = obj.Name,
                    BucketName = obj.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                // initialise client
                using var client = new AmazonS3Client(config);

                // initialise the transfer/upload tools
                var transferUtility = new TransferUtility(client);

                // initiate the file upload
                await transferUtility.UploadAsync(uploadRequest);

                response.StatusCode = 201;
                response.Message = $"{obj.Name} has been uploaded sucessfully";
            }
            catch (AmazonS3Exception s3Ex)
            {
                response.StatusCode = (int)s3Ex.StatusCode;
                response.Message = s3Ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Stream> DownloadFileAsync(S3Object obj)
        {
            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            };

            var response = new S3ResponseDto();
            try
            {
                var downloadStreamRequest = new TransferUtilityOpenStreamRequest()
                {
                    Key = obj.Name,
                    BucketName = obj.BucketName,
                };
                // initialise client
                using var client = new AmazonS3Client(config);

                // initialise the transfer/upload tools
                var transferUtility = new TransferUtility(client);

                // initiate the file upload
                var stream = await transferUtility.OpenStreamAsync(downloadStreamRequest);
                return stream;
            }
            catch (AmazonS3Exception s3Ex)
            {
                response.StatusCode = (int)s3Ex.StatusCode;
                response.Message = s3Ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }
            return null;
        }
    }
}
