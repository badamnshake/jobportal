using System;
using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience
{
    public class ReqAddExp
    {
        [Required] public int JobSeekerUserId { get; set; }
        [Required] [MaxLength(30)] public string CompanyName { get; set; }
        [Required] public string CompanyUrl { get; set; }
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
        public string Designation { get; set; }
        public string JobDescription { get; set; }
    }
}