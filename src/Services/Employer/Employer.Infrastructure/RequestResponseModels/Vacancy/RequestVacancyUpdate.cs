using System;

namespace Employer.Infrastructure.RequestResponseModels.Vacancy
{
    public class RequestVacancyUpdate
    {
        public int Id { get; set; }
        public int NoOfVacancies { get; set; }
        public string MinimumQualification { get; set; }
        public string JobDescription { get; set; }
        public string ExperienceRequired { get; set; }
        public string Location { get; set; }
        public DateTime LastDateToApply { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
    }
}