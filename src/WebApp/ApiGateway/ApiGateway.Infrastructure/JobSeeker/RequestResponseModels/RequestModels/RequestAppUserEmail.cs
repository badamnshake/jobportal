using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels
{
    public class RequestAppUserEmail
    {
        [Required] [EmailAddress] public string AppUserEmail { get; set; }
    }
}