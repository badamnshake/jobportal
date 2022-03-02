using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobSeeker.API.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int JobSeekerUserId { get; set; }
        public JobSeekerUser JobSeekerUser { get; set; }
        [Required]
        [MaxLength(30)]
        public string CompanyName { get; set; }
        [Required]
        [Column(TypeName ="Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Column(TypeName ="Date")]
        public DateTime EndDate { get; set; }
        public string CompanyUrl { get; set; }
        [Required]
        [MaxLength(30)]
        public string Designation { get; set; }
        public string JobDescription { get; set; }
    }
}