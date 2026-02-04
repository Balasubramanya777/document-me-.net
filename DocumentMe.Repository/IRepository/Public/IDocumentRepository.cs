using DocumentMe.DataAccessLayer.DTO.Document;
using DocumentMe.DataAccessLayer.Entity.Public;

namespace DocumentMe.Repository.IRepository.Public
{
    public interface IDocumentRepository
    {
        Task<Document> CreateDocument(Document document);
        Task<Document> UpdateDocument(Document document);
        Task<Document?> GetDocumentById(long documentId);
        Task<int?> GetDefaultIndex();
        Task<bool> IsDocumentExist(string title, long documentId);
        Task<List<DocumentUserDto>> GetDocuments(string? title);
        void CreateContent(List<DocumentUpdate> updates);
        Task<ContentDto?> GetContent(long DocumentId);
        Task UpsertSnapshot(long documentId, byte[] snapshotBytes);
        Task DeleteAllUpdates(long documentId);
    }
}
