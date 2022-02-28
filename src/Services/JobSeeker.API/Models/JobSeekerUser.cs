using System;
using System.Collections.Generic;

namespace JobSeeker.API.Models
{
    public class JobSeekerUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        // email which corresponds to the identity
        public string AppUserEmail { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int TotalExperience { get; set; }
        public int ExpectedSalaryAnnual { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Qualification> Qualifications { get; set; }
        public List<VacancyRequest> VacancyRequests { get; set; }
        public List<Experience> Experiences { get; set; }

    }
}