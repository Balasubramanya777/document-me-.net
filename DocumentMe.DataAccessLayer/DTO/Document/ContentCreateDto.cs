namespace DocumentMe.DataAccessLayer.DTO.Document
{
    public class ContentCreateDto
    {
        public long DocumentId { get; set; }
        public List<string>? Updates { get; set; }
        public string? Snapshot { get; set; }
    }
}
