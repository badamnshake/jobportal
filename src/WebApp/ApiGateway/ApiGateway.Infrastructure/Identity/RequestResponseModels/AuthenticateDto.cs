using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.Identity.RequestResponseModels
{
    public class AuthenticateDto
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}