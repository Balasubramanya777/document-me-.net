using DocumentMe.DataAccessLayer.Database;
using DocumentMe.DataAccessLayer.DTO.Document;
using DocumentMe.DataAccessLayer.DTO.Public;
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

        public async Task<Document?> GetDocumentById(long documentId)
        {
            return await _context.Documents.Where(x => x.DocumentId == documentId).FirstOrDefaultAsync();
        }

        public async Task<int?> GetDefaultIndex()
        {
            return await _context.Documents
                .Where(x => x.Title.StartsWith(Constants.MyNewDocumentWithSpace))
                .OrderByDescending(x => x.DocumentId)
                .Select(x => x.DefaultIndex)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsDocumentExist(string title, long documentId)
        {
            return await _context.Documents
                    .Where(x => x.Title.Equals(title) && x.DocumentId != documentId)
                    .AnyAsync();
        }

        public async Task<List<DocumentUserDto>> GetDocuments(string? title)
        {
            IQueryable<DocumentUserDto> query =
                from d in _context.Documents
                join u in _context.Users on d.CreatedBy equals u.UserId
                orderby d.LastSeenAt descending
                select new DocumentUserDto
                {
                    DocumentId = d.DocumentId,
                    Title = d.Title,
                    LastSeenAt = d.LastSeenAt,
                    User = new UserDto
                    {
                        UserId = u.UserId,
                        Username = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                    }
                };

            if(!string.IsNullOrWhiteSpace(title))
                query = query.Where(d => d.Title.Contains(title));

            List<DocumentUserDto> result = await query.ToListAsync();
            return result;
        }

        public async Task<bool> CreateContent(List<DocumentUpdate> updates)
        {
            try
            {
                await _context.DocumentUpdates.AddRangeAsync(updates);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ContentDto?> GetContent(long DocumentId)
        {

            return await _context.Documents
                .Where(d => d.DocumentId == DocumentId)
                .Select(d => new ContentDto
                {
                    DocumentId = d.DocumentId,
                    Title = d.Title,
                    Updates = d.DocumentUpdates
                        .OrderBy(u => u.DocumentUpdateId)
                        .Select(u => u.Content)
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

    }
}
