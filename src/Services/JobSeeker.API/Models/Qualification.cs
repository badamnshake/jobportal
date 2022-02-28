using System.ComponentModel.DataAnnotations;

namespace JobSeeker.API.Models
{
    public class Qualification
    {
        public int Id { get; set; }
        // fk to employer in vacancy tables
        public int JobSeekerUserId { get; set; }
        public JobSeekerUser JobSeekerUser { get; set; }
        public string QualificationName { get; set; }
        public string University { get; set; }
        public int YearOfCompletion { get; set; }
        [MaxLength(5)]
        public string GradeOrScore { get; set; }

    }
}
