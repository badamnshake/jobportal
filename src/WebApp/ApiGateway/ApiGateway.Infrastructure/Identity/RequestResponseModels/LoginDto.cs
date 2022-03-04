using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.Identity.RequestResponseModels
{
    public class LoginDto
    {
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}