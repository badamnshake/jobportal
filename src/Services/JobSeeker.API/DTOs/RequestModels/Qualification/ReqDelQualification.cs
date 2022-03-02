using System.ComponentModel.DataAnnotations;

namespace JobSeeker.API.DTOs
{
    public class ReqDelQualification
    {
        [Required]
        public int Id { get; set; }
    }
}