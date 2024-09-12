using JuniorHub.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace JuniorHub.Application.Contracts.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<BaseResponse<string?>> UploadMedia(IFormFile file);
    }
}
