using System;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification
{
    public class ResQualification
    {
        public int Id { get; set; }
        public string QualificationName { get; set; }
        public string University { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public string GradeOrScore { get; set; }
    }
}