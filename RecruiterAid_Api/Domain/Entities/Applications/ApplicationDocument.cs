namespace RecruiterAid_Api.Domain.Entities.Applications
{
    public class ApplicationDocument
    {
        public long ApplicationDocId { get; set; }
        public long WorkApplicationId { get; set; }
        public string? DocumentType { get; set; }
        public string FileUrl { get; set; } = null!;
        public string? FileHash { get; set; }
        public DateTime UploadedAt { get; set; }

        public WorkApplication WorkApplication { get; set; } = null!;
    }
}
