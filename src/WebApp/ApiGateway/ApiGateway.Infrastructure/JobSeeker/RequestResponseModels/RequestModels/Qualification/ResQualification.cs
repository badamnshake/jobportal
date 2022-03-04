using System;

namespace ApiGateway.Infrastructure.JobSeeker.RequestResponseModels.RequestModels.Qualification
{
    public class ResQualification
    {
        public string QualificationName { get; set; }
        public string University { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public string GradeOrScore { get; set; }
    }
}