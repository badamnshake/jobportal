using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.Employer.RequestResponseModels
{
    public class UserEmailDto
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}