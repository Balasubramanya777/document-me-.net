using AutoMapper;
using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Document;
using DocumentMe.DataAccessLayer.Entity.Public;
using DocumentMe.Repository.IRepository.Public;
using DocumentMe.Service.IService.Public;
using DocumentMe.Utility.Helper;
using DocumentMe.Utility.IUtility;
using DocumentMe.Utility.Resource;
using Microsoft.Extensions.Localization;
using System.Net;

namespace DocumentMe.Service.Service.Public
{
    public class DocumentService : IDocumentService
    {
        private readonly IStringLocalizer<Messages> _messagesLocalizer;
        private readonly IStringLocalizer<Labels> _labelsLocalizer;
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public DocumentService(IDocumentRepository documentRepository, IMapper mapper, IStringLocalizer<Messages> messagesLocalizer, IStringLocalizer<Labels> labelsLocalizer,
            ICurrentUser currentUser)
        {
            _mapper = mapper;
            _documentRepository = documentRepository;
            _messagesLocalizer = messagesLocalizer;
            _labelsLocalizer = labelsLocalizer;
            _currentUser = currentUser;
        }

        public async Task<ApiResponse<DocumentDTO>> CreateDocument()
        {
            string? userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                return new ApiResponse<DocumentDTO>(null, false, _messagesLocalizer["AccessDenied"], HttpStatusCode.Forbidden);
            }

            if (!long.TryParse(userId, out long id))
            {
                return new ApiResponse<DocumentDTO>(null, false, _messagesLocalizer["InternalServerError"], HttpStatusCode.InternalServerError);
            }

            DateTime now = DateTime.UtcNow;
            int? defaultIndex = await _documentRepository.GetDefaultIndex(id);

            if (defaultIndex != null)
            {
                ++defaultIndex;
            }
            else
            {
                defaultIndex = 1;
            }

            Document document = new()
            {
                Title = Constants.MyNewDocumentWithSpace + defaultIndex,
                DefaultIndex = defaultIndex,
                LastSeenAt = now,
                CreatedBy = id,
                CreatedAt = now
            };
            document = await _documentRepository.CreateDocument(document);

            return ApiResponse<DocumentDTO>.Builder()
                .Data(new DocumentDTO { Title = document.Title, DocumentId = document.DocumentId })
                .Success(true)
                .Message(_messagesLocalizer["SaveSuccess", _labelsLocalizer["Document"]])
                .Code(HttpStatusCode.Created)
                .Build();
        }

        public async Task UpdateDocument()
        {

            //_documentRepository.IsDocumentExist()

        }
    }
}
