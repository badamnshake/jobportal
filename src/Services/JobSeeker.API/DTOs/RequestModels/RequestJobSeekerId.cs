
using System.ComponentModel.DataAnnotations;

namespace JobSeeker.API.DTOs
{
    public class RequestJobSeekerId
    {
        [Required]
        public int Id {get; set;}
    }
}