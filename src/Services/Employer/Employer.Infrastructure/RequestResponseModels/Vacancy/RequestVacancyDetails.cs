using System;
using System.ComponentModel.DataAnnotations;

namespace Employer.Infrastructure.RequestResponseModels.Vacancy
{
    public class RequestVacancyDetails
    {
        // id is automatically assigned to as primary key in entity framework
        // organization name which it is published by

        // public DateTime PublishedDate { get; set; }
        [Required] public int NoOfVacancies { get; set; }
        [Required] public string MinimumQualification { get; set; }
        [Required] public string JobDescription { get; set; }
        public string ExperienceRequired { get; set; }
        [Required] public string Location { get; set; }
        [Required] public DateTime LastDateToApply { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
    }
}