using System;
using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Infrastructure.JobSeeker.RequestResponseModels.RequestModels
{
    public class RequestJobSeekerUser
    {
        [Required] [MaxLength(30)] public string FirstName { get; set; }
        [Required] [MaxLength(30)] public string LastName { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        // email which corresponds to the identity
        [Required] [EmailAddress] public string AppUserEmail { get; set; }
        [MaxLength(15)] public string Phone { get; set; }
        [MaxLength(100)] public string Address { get; set; }
        [Required] public int TotalExperience { get; set; }
        [Required] public int ExpectedSalaryAnnual { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
    }
}