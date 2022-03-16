using System;

namespace JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels
{
    public class ResExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CompanyUrl { get; set; }
        public string Designation { get; set; }
        public string JobDescription { get; set; }
    }
}