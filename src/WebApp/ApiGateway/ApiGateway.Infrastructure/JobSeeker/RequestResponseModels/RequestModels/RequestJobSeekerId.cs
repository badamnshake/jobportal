using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.JobSeeker.RequestResponseModels.RequestModels
{
    public class RequestJobSeekerId
    {
        [Required] public int Id { get; set; }
    }
}