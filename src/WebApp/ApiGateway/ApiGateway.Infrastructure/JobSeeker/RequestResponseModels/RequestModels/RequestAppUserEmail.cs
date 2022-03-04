using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.JobSeeker.RequestResponseModels.RequestModels
{
    public class RequestAppUserEmail
    {
        [Required] [EmailAddress] public string AppUserEmail { get; set; }
    }
}