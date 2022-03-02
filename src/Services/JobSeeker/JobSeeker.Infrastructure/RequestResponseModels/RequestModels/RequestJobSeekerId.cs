using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels
{
    public class RequestJobSeekerId
    {
        [Required] public int Id { get; set; }
    }
}