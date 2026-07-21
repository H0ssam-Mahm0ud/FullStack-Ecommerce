using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Shared.Contracts;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string folderName);
    void DeleteFile(string filePath);
}
