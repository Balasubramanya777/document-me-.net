using DocumentMe.API.Helper;
using DocumentMe.Service.IService.Public;
using DocumentMe.Utility.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DocumentMe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPost("CreateDocument")]
        public async Task<IActionResult> CreateDocument()
        {
            var response = await _documentService.CreateDocument();
            return response.ToActionResult();
        }
    }
}
