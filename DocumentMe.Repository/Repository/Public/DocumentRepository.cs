using DocumentMe.DataAccessLayer.Database;
using DocumentMe.DataAccessLayer.Entity.Public;
using DocumentMe.Repository.IRepository.Public;
using DocumentMe.Utility.Helper;
using Microsoft.EntityFrameworkCore;

namespace DocumentMe.Repository.Repository.Public
{
    public class DocumentRepository : IDocumentRepository
    {
        protected readonly DocumentMeDBContext _context;
        public DocumentRepository(DocumentMeDBContext context)
        {
            _context = context;
        }

        public async Task<Document> CreateDocument(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> UpdateDocument(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<int?> GetDefaultIndex(long createdBy)
        {
            return await _context.Documents
                .Where(x => x.CreatedBy == createdBy && x.Title.StartsWith(Constants.MyNewDocumentWithSpace))
                .Select(x => x.DefaultIndex).FirstOrDefaultAsync();
        }

        public async Task<bool> IsDocumentExist(string title, long documentId, long createdBy)
        {
            return await _context.Documents
                    .Where(x => x.Title.Equals(title) && x.CreatedBy == createdBy && x.DocumentId != documentId)
                    .AnyAsync();
        }

    }
}
