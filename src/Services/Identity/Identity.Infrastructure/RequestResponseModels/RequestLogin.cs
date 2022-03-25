using System.ComponentModel.DataAnnotations;

namespace Identity.Infrastructure.RequestResponseModels
{
    public class RequestLogin
    {
        [Required] [EmailAddress] public string Email { get; set; }
        [Required] [MinLength(5)] public string Password { get; set; }
    }
}