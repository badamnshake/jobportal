
using System.ComponentModel.DataAnnotations;

namespace Employer.API.DTOs
{
    public class UserEmailDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }
}