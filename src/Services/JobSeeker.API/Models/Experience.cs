using System;
using System.ComponentModel.DataAnnotations;

namespace JobSeeker.API.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int JobSeekerUserId { get; set; }
        public JobSeekerUser JobSeekerUser { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CompanyUrl { get; set; }
        public string Designation { get; set; }
        public string JobDescription { get; set; }
    }
}