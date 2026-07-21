using Ecom.Application.Shared.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Application.Shared.Services;

public class FileService : IFileService
{
    private readonly IFileProvider _fileProvider;

    public FileService(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0) return string.Empty;

        var folderInfo = _fileProvider.GetFileInfo(folderName);
        var physicalFolder = folderInfo.PhysicalPath;

        if (string.IsNullOrEmpty(physicalFolder))
        {
            throw new InvalidOperationException("The configured IFileProvider does not support physical paths.");
        }

        if (!Directory.Exists(physicalFolder))
        {
            Directory.CreateDirectory(physicalFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        var physicalFilePath = Path.Combine(physicalFolder, uniqueFileName);

        using (var stream = new FileStream(physicalFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
    }

    public void DeleteFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return;

        var fileInfo = _fileProvider.GetFileInfo(filePath);
        var physicalPath = fileInfo.PhysicalPath;

        if (!string.IsNullOrEmpty(physicalPath) && File.Exists(physicalPath))
        {
            File.Delete(physicalPath);
        }
    }
}