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

        public async Task<ApiResponse<DocumentUpsertDto>> CreateDocument()
        {
            string? userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return new ApiResponse<DocumentUpsertDto>(null, false, _messagesLocalizer["AuthAccessDenied"], HttpStatusCode.Forbidden);

            if (!long.TryParse(userId, out long id))
                return new ApiResponse<DocumentUpsertDto>(null, false, _messagesLocalizer["ErrorInternalServerError"], HttpStatusCode.InternalServerError);

            DateTimeOffset now = DateTimeOffset.UtcNow;
            int? defaultIndex = await _documentRepository.GetDefaultIndex();

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

            return ApiResponse<DocumentUpsertDto>.Builder()
                .Data(new DocumentUpsertDto { Title = document.Title, DocumentId = document.DocumentId })
                .Success(true)
                .Message(_messagesLocalizer["ResponseSaveSuccess", _labelsLocalizer["Document"]])
                .Code(HttpStatusCode.Created)
                .Build();
        }

        public async Task<ApiResponse<DocumentUpsertDto>> UpdateDocument(DocumentUpsertDto documentUpsertDto)
        {
            string? userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return new ApiResponse<DocumentUpsertDto>(null, false, _messagesLocalizer["AuthAccessDenied"], HttpStatusCode.Forbidden);

            if (string.IsNullOrWhiteSpace(documentUpsertDto.Title) || documentUpsertDto.DocumentId == default)
                return new ApiResponse<DocumentUpsertDto>(null, false, _messagesLocalizer["ErrorMissingField"], HttpStatusCode.BadRequest);

            bool isExist =await _documentRepository.IsDocumentExist(documentUpsertDto.Title, documentUpsertDto.DocumentId);
            if(isExist)
                return new ApiResponse<DocumentUpsertDto>(null, false, _messagesLocalizer["ErrorAlreadyExistsWith", _labelsLocalizer["Document"], _labelsLocalizer["Title"], documentUpsertDto.Title], HttpStatusCode.BadRequest);

            Document? document = await _documentRepository.GetDocumentById(documentUpsertDto.DocumentId);
            if (document == null)
                return new ApiResponse<DocumentUpsertDto>(null, false, _messagesLocalizer["ErrorNotFound"], HttpStatusCode.BadRequest);

            DateTimeOffset now = DateTimeOffset.UtcNow;
            document.Title = documentUpsertDto.Title;
            document.LastSeenAt = now;
            document.UpdatedAt = now;

            await _documentRepository.UpdateDocument(document);
            return ApiResponse<DocumentUpsertDto>.Builder()
                .Data(documentUpsertDto)
                .Success(true)
                .Message(_messagesLocalizer["ResponseUpdateSuccess", _labelsLocalizer["Document"]])
                .Code(HttpStatusCode.OK)
                .Build();
        }
        public async Task<ApiResponse<List<DocumentUserDto>>> GetDocuments(string? title)
        {
            List<DocumentUserDto> result = await _documentRepository.GetDocuments(title);
            return ApiResponse<List<DocumentUserDto>>.Builder()
                .Data(result)
                .Success(true)
                .Message(_messagesLocalizer["ResponseRetrieveSuccess"])
                .Code(HttpStatusCode.OK)
                .Build();
        }

        public async Task<ApiResponse<bool>> CreateContent(ContentCreateDto contentDto)
        {
            string? userId = _currentUser.UserId;
            if (string.IsNullOrEmpty(userId))
                return new ApiResponse<bool>(false, false, _messagesLocalizer["AuthAccessDenied"], HttpStatusCode.Forbidden);

            if (!long.TryParse(userId, out long id))
                return new ApiResponse<bool>(false, false, _messagesLocalizer["ErrorInternalServerError"], HttpStatusCode.InternalServerError);


            List<DocumentUpdate> updates = [];
            DateTimeOffset now = DateTimeOffset.UtcNow;
            if (contentDto.Updates != null && contentDto.Updates.Count > default(int))
            {
                foreach (string update in contentDto.Updates)
                {
                    DocumentUpdate docUpdate = new()
                    {
                        DocumentId = contentDto.DocumentId,
                        Content = Convert.FromBase64String(update),
                        CreatedBy = id,
                        CreatedAt = now,
                    };
                    updates.Add(docUpdate);
                }
                bool isSuccess = await _documentRepository.CreateContent(updates);
                if (!isSuccess)
                    return new ApiResponse<bool>(false, false, _messagesLocalizer["ErrorInternalServerError"], HttpStatusCode.InternalServerError);

            }

            return ApiResponse<bool>.Builder()
                .Data(true)
                .Success(true)
                .Message(_messagesLocalizer["ResponseSaveSuccess", _labelsLocalizer["Content"]])
                .Code(HttpStatusCode.Created)
                .Build();
        }

        public async Task<ApiResponse<ContentDto>> GetContent(long documentId)
        {
            ContentDto? content = await _documentRepository.GetContent(documentId);
            if (content == null)
                return new ApiResponse<ContentDto>(null, false, _messagesLocalizer["ErrorInternalServerError"], HttpStatusCode.InternalServerError);

            Document? document = await _documentRepository.GetDocumentById(documentId);
            if (document == null)
                return new ApiResponse<ContentDto>(null, false, _messagesLocalizer["ErrorInternalServerError"], HttpStatusCode.InternalServerError);

            document.LastSeenAt = DateTimeOffset.UtcNow;
            await _documentRepository.UpdateDocument(document);

            return ApiResponse<ContentDto>.Builder()
                .Data(content)
                .Success(true)
                .Message(_messagesLocalizer["ResponseRetrieveSuccess"])
                .Code(HttpStatusCode.OK)
                .Build();
        }
    }
}
