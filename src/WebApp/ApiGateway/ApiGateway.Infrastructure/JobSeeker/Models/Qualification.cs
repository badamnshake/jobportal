using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGateway.Infrastructure.JobSeeker.Models
{
    public class Qualification
    {
        public int Id { get; set; }

        // fk to employer in vacancy tables
        public int JobSeekerUserId { get; set; }
        public JobSeekerUser JobSeekerUser { get; set; }
        [Required] [MaxLength(30)] public string QualificationName { get; set; }
        public string University { get; set; }
        [Required] [Column(TypeName = "Date")] public DateTime DateOfCompletion { get; set; }
        [MaxLength(5)] public string GradeOrScore { get; set; }
    }
}