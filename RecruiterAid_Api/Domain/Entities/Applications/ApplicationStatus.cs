namespace RecruiterAid_Api.Domain.Entities.Applications
{
    public class ApplicationStatus
    {
        public long StatusId { get; set; }
        public long WorkApplicationId { get; set; }
        public string? OldStatus { get; set; }
        public string NewStatus { get; set; } = null!;
        public DateTime ChangedAt { get; set; }
        public long? ChangedByUserId { get; set; }
        public string? Reason { get; set; }
        public string? Notes { get; set; }

        public WorkApplication WorkApplication { get; set; } = null!;
    }
}

