using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels
{
    public class RequestJobSeekerDelete
    {
        
        [Required] [EmailAddress] public string Email { get; set; }
    }
}