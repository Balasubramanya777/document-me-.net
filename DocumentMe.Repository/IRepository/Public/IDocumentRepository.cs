using DocumentMe.DataAccessLayer.Entity.Public;

namespace DocumentMe.Repository.IRepository.Public
{
    public interface IDocumentRepository
    {
        Task<Document> CreateDocument(Document document);
        Task<Document> UpdateDocument(Document document);
        Task<int?> GetDefaultIndex(long createdBy);
        Task<bool> IsDocumentExist(string title, long documentId, long createdBy);
    }
}
