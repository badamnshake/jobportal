using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.Employer.Models
{
    public class EmployerEntity
    {
        // id is automatically assigned to as primary key in entity framework
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        [EmailAddress] public string CompanyEmail { get; set; }
        [MaxLength(15)] public string CompanyPhone { get; set; }
        public int NoOfEmployees { get; set; }
        public int StartYear { get; set; }
        public string About { get; set; }

        // email address of the account
        [Required] [EmailAddress] public string CreatedByEmailUser { get; set; }

        // by doing this vacancy columns cascade and depend on employer entity to work
        public List<Vacancy> Vacancies { get; set; }
    }
}