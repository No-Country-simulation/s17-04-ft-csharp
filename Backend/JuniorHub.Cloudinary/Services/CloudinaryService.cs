using JuniorHub.Application.Contracts.Cloudinary;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using JuniorHub.Application.DTOs;

namespace JuniorHub.Cloudinary.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        
        public CloudinaryService(Helpers.CloudinaryConfiguration cloudinary)
        {
            var cloudAccount = new Account(cloudinary.Cloud, cloudinary.ApiKey, cloudinary.ApiSecret);
            _cloudinary = new CloudinaryDotNet.Cloudinary(cloudAccount);
        }
        public async Task<BaseResponse<string?>> UploadMedia(IFormFile file)
        {
            BaseResponse<string?> baseResponse;

            if (file is null || file.Length == 0)
            {
                return baseResponse = new BaseResponse<string?>(null, false, "File is null or empty.", null);
            }

            using var stream = file.OpenReadStream();

            var uploadParams = ConfigureImageTransformation(new Transformation().Height(250).Width(250).Crop("scale"), stream, file.FileName);

            try
            {
                var resultString = await Upload(uploadParams);
                baseResponse = new BaseResponse<string?>(resultString, true, "", null);
            }
            catch
            {
                baseResponse = new BaseResponse<string?>(null, false, "An error occurred while uploading the file.", null);
            }
            return baseResponse;
        }

        private async Task<string?> Upload(RawUploadParams uploadParams)
        {
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.Url.ToString();
        }

        private ImageUploadParams ConfigureImageTransformation(Transformation transformation, Stream stream, string name)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription
                {
                    FileName = name,
                    Stream = stream
                },
                Transformation = transformation
            };
            return uploadParams;
        }
    }
}
