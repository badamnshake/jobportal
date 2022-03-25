using System.ComponentModel.DataAnnotations;

namespace Identity.Infrastructure.RequestResponseModels
{
    public class RequestChangePassword
    {
        // one thing can be added which is having old and new password
        // but as relying on token authentication is done with that
        [Required] [MinLength(5)] public string NewPassword { get; set; }
    }
}