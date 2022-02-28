using System.ComponentModel.DataAnnotations;

namespace Employer.API.DTOs
{
    public class DetailsDto
    {
        [Required]
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        [EmailAddress]
        public string CompanyEmail { get; set; }
        [MaxLength(15)]
        public string CompanyPhone { get; set; }
        public int NoOfEmployees { get; set; }
        public int StartYear { get; set; }
        public string About { get; set; }

        [Required]
        [EmailAddress]
        public string CreatedByEmailUser { get; set; }
    }
}