using System.ComponentModel.DataAnnotations;

namespace Identity.Infrastructure.RequestResponseModels
{
    public class AuthenticateDto
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}