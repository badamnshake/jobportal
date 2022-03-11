using System.ComponentModel.DataAnnotations;

namespace Employer.Infrastructure.RequestResponseModels.Employer
{
    public class RequestEmployerDelete
    {
        [Required] public string Email { get; set; }
    }
}