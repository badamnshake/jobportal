using System.ComponentModel.DataAnnotations;

namespace Employer.Infrastructure.RequestResponseModels
{
    public class UserEmailDto
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}