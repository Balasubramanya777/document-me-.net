using DocumentMe.DataAccessLayer.DTO.Public;

namespace DocumentMe.DataAccessLayer.DTO.Document
{
    public class DocumentUserDto
    {
        public long DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTimeOffset LastSeenAt { get; set; }
        public UserDto? User { get; set; }
    }
}
