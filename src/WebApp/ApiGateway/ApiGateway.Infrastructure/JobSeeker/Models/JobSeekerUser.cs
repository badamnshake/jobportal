using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGateway.Infrastructure.JobSeeker.Models
{
    public class JobSeekerUser
    {
        public int Id { get; set; }
        [Required] [MaxLength(30)] public string FirstName { get; set; }
        [Required] [MaxLength(30)] public string LastName { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        // email which corresponds to the identity
        [Required] [EmailAddress] public string AppUserEmail { get; set; }
        [MaxLength(15)] public string Phone { get; set; }
        [MaxLength(100)] public string Address { get; set; }
        public int TotalExperience { get; set; }
        public int ExpectedSalaryAnnual { get; set; }
        [Column(TypeName = "Date")] public DateTime DateOfBirth { get; set; }
        public List<Qualification> Qualifications { get; set; }
        public List<VacancyRequest> VacancyRequests { get; set; }
        public List<Experience> Experiences { get; set; }
    }
}