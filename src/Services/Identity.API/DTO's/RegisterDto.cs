
using System.ComponentModel.DataAnnotations;
using Identity.API.Entities;

namespace Identity.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Range(0, 1)]
        public int UserType { get; set; }
    }
}