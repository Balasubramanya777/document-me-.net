using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentMe.DataAccessLayer.Entity.Public
{
    [Table("document_permission", Schema = "public")]
    [PrimaryKey(nameof(DocumentId), nameof(UserId))]
    public class DocumentPermission
    {
        [Column("document_id")]
        public long DocumentId { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [Column("role")]
        public int Role { get; set; }


        public virtual Document? Document { get; set; }
        public virtual User? User { get; set; }
    }
}
