using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentMe.DataAccessLayer.Entity.Public
{
    [Table("document_snapshot", Schema = "public")]
    public class DocumentSnapshot
    {
        [Key]
        [Column("document_id")]
        public long DocumentId { get; set; }

        [Column("snapshot")]
        public byte[] Snapshot { get; set; } = [];

        [Column("version")]
        public long? Version { get; set; }

        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }


        [ForeignKey(nameof(DocumentId))]
        public virtual Document? Document { get; set; }
    }
}
