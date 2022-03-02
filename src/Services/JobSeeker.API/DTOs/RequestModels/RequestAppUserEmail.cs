using System.ComponentModel.DataAnnotations;

namespace JobSeeker.API.DTOs
{
    public class RequestAppUserEmail
    {
        [Required]
        [EmailAddress]
        public string AppUserEmail {get; set;}
    }
}