using System;
using System.Collections.Generic;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;

namespace JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels
{
    public class ResponseJobSeekerUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int TotalExperience { get; set; }
        public int ExpectedSalaryAnnual { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<ResQualification> Qualifications { get; set; }
        public List<ResExperience> Experiences { get; set; }
        // public List<int> AppliedVacanciesId { get; set; }
    }
}