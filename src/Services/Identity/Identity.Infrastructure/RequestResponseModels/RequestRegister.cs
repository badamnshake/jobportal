using System.ComponentModel.DataAnnotations;

namespace Identity.Infrastructure.RequestResponseModels
{
    public class RequestRegister
    {
        [Required] public string FullName { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string UserName { get; set; }
        [Required][MinLength(5)] public string Password { get; set; }

        [Required] [MaxLength(15)] public string Phone { get; set; }

        [Range(0, 1)] public int UserType { get; set; }
    }
}