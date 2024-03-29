﻿using Application.Photos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile formFile);
        Task<string> DeletePhoto(string publicId);
    }
}
