using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentMe.DataAccessLayer.Entity.Public
{
    [Table("document", Schema = "public")]
    public class Document
    {
        [Key]
        [Column("document_id")]
        public long DocumentId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("default_index")]
        public int? DefaultIndex { get; set; }

        [Column("last_seen_at")]
        public DateTime LastSeenAt { get; set; }

        [Column("created_by")]
        public long? CreatedBy { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
