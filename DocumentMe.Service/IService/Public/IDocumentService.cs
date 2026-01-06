using Base.DataAccessLayer.DTO.Base;
using DocumentMe.DataAccessLayer.DTO.Document;

namespace DocumentMe.Service.IService.Public
{
    public interface IDocumentService
    {
        Task<ApiResponse<DocumentDTO>> CreateDocument();
    }
}
