using Microsoft.AspNetCore.Http;

namespace Ecom.Application.Shared.Contracts;

public interface IFileService
{
    Task<List<string>> SaveFileAsync(IFormFileCollection files, string src);
    void DeleteFile(string src);
}
