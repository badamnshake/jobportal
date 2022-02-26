
using System.ComponentModel.DataAnnotations;

namespace Identity.API.DTOs
{
    public class AuthenticateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}