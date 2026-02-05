using DocumentMe.DataAccessLayer.Entity.Public;
using Microsoft.EntityFrameworkCore;

namespace DocumentMe.DataAccessLayer.Database
{
    public class DocumentMeDBContext : DbContext
    {
        public DocumentMeDBContext(DbContextOptions<DocumentMeDBContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentUpdate> DocumentUpdates { get; set; }
        public DbSet<DocumentSnapshot> DocumentSnapshots { get; set; }
    }
}
