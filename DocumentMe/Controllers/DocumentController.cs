using DocumentMe.API.Helper;
using DocumentMe.DataAccessLayer.DTO.Document;
using DocumentMe.Service.IService.Public;
using DocumentMe.Utility.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DocumentMe.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IStringLocalizer<Messages> _messagesLocalizer;
        public DocumentController(IDocumentService documentService, IStringLocalizer<Messages> localizer)
        {
            _documentService = documentService;
            _messagesLocalizer = localizer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument()
        {
            var response = await _documentService.CreateDocument();
            return response.ToActionResult();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateDocument(DocumentUpsertDto documentUpsertDto)
        {
            var response = await _documentService.UpdateDocument(documentUpsertDto);
            return response.ToActionResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            var response = await _documentService.GetDocuments();
            return response.ToActionResult();
        }
    }
}
