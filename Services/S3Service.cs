using Amazon.S3;
using Amazon.S3.Model;

namespace ImaGen_BE.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;
        private readonly string? _bucket;

        public S3Service(IAmazonS3 s3, IConfiguration config) { 
            _s3Client = s3;
            _config = config;
            _bucket = _config["AWS:BucketName"];
        }

        public async Task<string> UploadImage(string base64, string fileName)
        {
            var base64Data = base64.Contains(",") ? base64.Split(',')[1] : base64;
            var bytes = Convert.FromBase64String(base64Data);
            using var stream = new MemoryStream(bytes);

            var request = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = fileName,
                InputStream = stream,
                ContentType = "image/png"
            };

            await _s3Client.PutObjectAsync(request);

            return $"https://{_bucket}.s3.amazonaws.com/{fileName}";
        }
    }
}
