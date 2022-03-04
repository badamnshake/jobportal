using System;

namespace ApiGateway.Infrastructure.Employer.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string PublishedBy { get; set; }
        public DateTime PublishedDate { get; set; }
        public int NoOfVacancies { get; set; }
        public string MinimumQualification { get; set; }
        public string JobDescription { get; set; }
        public string ExperienceRequired { get; set; }
        public string Location { get; set; }
        public DateTime LastDateToApply { get; set; }
        public int MinSalary { get; set; }

        public int MaxSalary { get; set; }

        // foreign key to employer entity config
        public int EmployerEntityId { get; set; }
        public EmployerEntity EmployerEntity { get; set; }
    }
}