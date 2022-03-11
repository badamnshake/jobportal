using System.ComponentModel.DataAnnotations;

namespace Identity.Infrastructure.RequestResponseModels
{
    public class RequestDelete
    {
        [Required] [EmailAddress] public string email { get; set; }
        
    }
}