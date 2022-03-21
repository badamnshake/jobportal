using System;
using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification
{
    public class ReqAddQualification
    {
        [Required] public string QualificationName { get; set; }
        public string University { get; set; }
        [Required] public DateTime DateOfCompletion { get; set; }
        [MaxLength(5)] public string GradeOrScore { get; set; }
    }
}