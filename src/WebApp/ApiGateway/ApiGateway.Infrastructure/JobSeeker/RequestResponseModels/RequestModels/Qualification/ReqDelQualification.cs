using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.JobSeeker.RequestResponseModels.RequestModels.Qualification
{
    public class ReqDelQualification
    {
        [Required] public int Id { get; set; }
    }
}