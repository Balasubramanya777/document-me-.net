namespace DocumentMe.DataAccessLayer.DTO.Document
{
    public class ContentDto
    {
        public long DocumentId { get; set; }
        public string? Title { get; set; }
        public List<byte[]> Updates { get; set; } = [];
        public string? Snapshot { get; set; }
    }
}
