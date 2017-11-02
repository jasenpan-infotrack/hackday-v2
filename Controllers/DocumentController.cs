using Microsoft.AspNetCore.Mvc;
using DocumentVerification.Interfaces;
using DocumentVerification.Models;
using Microsoft.AspNetCore.Http;

namespace DocumentVerification.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("[action]")]
        public Document ValidPhoto(IFormFile file)
        {
            //var path = "C:\\Users\\jasen.zheng\\Desktop\\Sample\\Licences\\4c381656-70d9-4386-85ef-bc986d881730.jpg";
            //var path2 = "C:\\Users\\jasen.zheng\\Desktop\\Sample\\Licences\\4e005ba0-a2a5-4389-afce-0e3880a074a4.jpg";
            //var path3 = "C:\\Users\\jasen.zheng\\Desktop\\Sample\\Licences\\54ed5182-77ef-4c98-bdf0-7cc4e19e6f8b.jpg";
            //var path4 = "C:\\Users\\jasen.zheng\\Desktop\\Sample\\Licences\\61de63bf-030a-4e8f-b370-b4ccdc56c6a6.jpg";
            //var path5 = "C:\\Users\\jasen.zheng\\Desktop\\Sample\\Licences\\119585f2-5f75-40f3-8704-fbb09eb57fb7.jpg";
            return _documentService.FetchDataFromPhoto(file);
            //var model2 = _documentService.FetchDataFromPhoto(path2);
            //var model3 = _documentService.FetchDataFromPhoto(path3);
            //var model4 = _documentService.FetchDataFromPhoto(path4);
            //var model5 = _documentService.FetchDataFromPhoto(path5);
        }
    }
}