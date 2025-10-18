namespace RecruiterAid_Api.Domain.Entities
{
    public class Candidate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ResumePath { get; set; }
        public string AssignedAgentId { get; set; }
        public string Status { get; set; }
    }
}
