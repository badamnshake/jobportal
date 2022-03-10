using System;

namespace VacancyRequests.Aggregator.Models.Responses
{
    public class ResQualification
    {
        
        public string QualificationName { get; set; }
        public string University { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public string GradeOrScore { get; set; }
    }
}