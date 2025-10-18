using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Domain.Entities.Offers
{
    public class Offer
    {
        public long OfferId { get; set; }
        public long WorkApplicationId { get; set; }
        public string OfferStatus { get; set; } = "draft";
        public decimal? OfferedSalary { get; set; }
        public string? OfferedJobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string? OfferLetterUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public WorkApplication WorkApplication { get; set; } = null!;
    }
}
