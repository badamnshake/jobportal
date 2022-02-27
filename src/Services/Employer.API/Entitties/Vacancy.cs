


using System;
using System.ComponentModel.DataAnnotations;

namespace Employer.API.Entities
{
    public class Vacancy
    {
        // id is automatically assigned to as primary key in entity framework
        public int Id { get; set; }
        // organization name which it is published by
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
    }

}