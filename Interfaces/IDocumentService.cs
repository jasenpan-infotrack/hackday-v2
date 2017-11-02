
using DocumentVerification.Models;
using Microsoft.AspNetCore.Http;

namespace DocumentVerification.Interfaces
{
    public interface IDocumentService
    {
        Document FetchDataFromPhoto(IFormFile file);
    }
}
