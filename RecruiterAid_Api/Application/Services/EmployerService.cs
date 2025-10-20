using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities.Employers;
using RecruiterAid_Api.Infrastructure.Data;
using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Application.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly ApplicationDbContext _db;

        public EmployerService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<EmployerDto>> GetAllAsync()
        {
            return await _db.Employers
                .Select(e => new EmployerDto
                {
                    EmployerId = e.EmployerId,
                    Name = e.Name,
                    Industry = e.Industry,
                    WebsiteUrl = e.WebsiteUrl,
                    ContactPerson = e.ContactPerson,
                    ContactEmail = e.ContactEmail,
                    ContactPhone = e.ContactPhone,
                    GSTNumber = e.GSTNumber,
                    PANNumber = e.PANNumber,
                    TANNumber = e.TANNumber,
                    BillingAddress = e.BillingAddress,
                    BankAccountNumber = e.BankAccountNumber,
                    IFSCCode = e.IFSCCode,
                    PaymentTerms = e.PaymentTerms
                })
                .ToListAsync();
        }

        public async Task<EmployerDto?> GetByIdAsync(long id)
        {
            var e = await _db.Employers.FindAsync(id);
            if (e == null) return null;

            return new EmployerDto
            {
                EmployerId = e.EmployerId,
                Name = e.Name,
                Industry = e.Industry,
                WebsiteUrl = e.WebsiteUrl,
                ContactPerson = e.ContactPerson,
                ContactEmail = e.ContactEmail,
                ContactPhone = e.ContactPhone,
                GSTNumber = e.GSTNumber,
                PANNumber = e.PANNumber,
                TANNumber = e.TANNumber,
                BillingAddress = e.BillingAddress,
                BankAccountNumber = e.BankAccountNumber,
                IFSCCode = e.IFSCCode,
                PaymentTerms = e.PaymentTerms
            };
        }

        public async Task<EmployerDto> CreateAsync(CreateEmployerDto dto)
        {
            var employer = new Employer
            {
                Name = dto.Name,
                Industry = dto.Industry,
                WebsiteUrl = dto.WebsiteUrl,
                ContactPerson = dto.ContactPerson,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                GSTNumber = dto.GSTNumber,
                PANNumber = dto.PANNumber,
                TANNumber = dto.TANNumber,
                BillingAddress = dto.BillingAddress,
                BankAccountNumber = dto.BankAccountNumber,
                IFSCCode = dto.IFSCCode,
                PaymentTerms = dto.PaymentTerms,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _db.Employers.Add(employer);
            await _db.SaveChangesAsync();

            return await GetByIdAsync(employer.EmployerId);
        }

        public async Task<EmployerDto?> UpdateAsync(long id, CreateEmployerDto dto)
        {
            var employer = await _db.Employers.FindAsync(id);
            if (employer == null) return null;

            employer.Name = dto.Name;
            employer.Industry = dto.Industry;
            employer.WebsiteUrl = dto.WebsiteUrl;
            employer.ContactPerson = dto.ContactPerson;
            employer.ContactEmail = dto.ContactEmail;
            employer.ContactPhone = dto.ContactPhone;
            employer.GSTNumber = dto.GSTNumber;
            employer.PANNumber = dto.PANNumber;
            employer.TANNumber = dto.TANNumber;
            employer.BillingAddress = dto.BillingAddress;
            employer.BankAccountNumber = dto.BankAccountNumber;
            employer.IFSCCode = dto.IFSCCode;
            employer.PaymentTerms = dto.PaymentTerms;
            employer.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return await GetByIdAsync(employer.EmployerId);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var employer = await _db.Employers.FindAsync(id);
            if (employer == null) return false;

            _db.Employers.Remove(employer);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
