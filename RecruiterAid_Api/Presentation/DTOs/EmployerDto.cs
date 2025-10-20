using System.ComponentModel.DataAnnotations;

namespace RecruiterAid_Api.Presentation.DTOs
{
    public class EmployerDto
    {
        public long EmployerId { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string WebsiteUrl { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string TANNumber { get; set; }
        public string BillingAddress { get; set; }
        public string BankAccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PaymentTerms { get; set; }
    }

    public class CreateEmployerDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public string Industry { get; set; }
        public string WebsiteUrl { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string TANNumber { get; set; }
        public string BillingAddress { get; set; }
        public string BankAccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PaymentTerms { get; set; }
    }
}
