using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.JobSeeker.RequestResponseModels.RequestModels.Experience
{
    public class ReqDelExp
    {
        [Required] public int Id { get; set; }
    }
}