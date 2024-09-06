using JunioHub.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Contracts.Cloudinary
{
    public interface ICloudinaryService
    {
        Task<BaseResponse<string?>> UploadMedia(IFormFile file);
    }
}
