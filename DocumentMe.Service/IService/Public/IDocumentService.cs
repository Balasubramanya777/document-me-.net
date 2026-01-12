using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Document;

namespace DocumentMe.Service.IService.Public
{
    public interface IDocumentService
    {
        Task<ApiResponse<DocumentUpsertDto>> CreateDocument();
        Task<ApiResponse<DocumentUpsertDto>> UpdateDocument(DocumentUpsertDto documentUpsertDto);
        Task<ApiResponse<List<DocumentUserDto>>> GetDocuments();
    }
}
