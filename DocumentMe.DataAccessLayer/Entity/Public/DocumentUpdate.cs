using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentMe.DataAccessLayer.Entity.Public
{
    [Table("document_update", Schema = "public")]
    public class DocumentUpdate
    {
        [Key]
        [Column("document_update_id")]
        public long DocumentUpdateId { get; set; }

        [Required]
        [Column("document_id")]
        public long DocumentId { get; set; }

        [Column("content")]
        public byte[] Content { get; set; } = [];

        [Column("created_by")]
        public long? CreatedBy { get; set; }

        [Column("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }



        [ForeignKey(nameof(DocumentId))]
        public virtual Document? Document { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User? CreatedByNavigation { get; set; }
    }
}
