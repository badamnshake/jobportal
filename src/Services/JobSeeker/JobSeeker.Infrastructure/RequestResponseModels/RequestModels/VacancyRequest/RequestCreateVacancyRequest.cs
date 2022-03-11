using System.ComponentModel.DataAnnotations;

namespace JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.VacancyRequest
{
    public class RequestCreateVacancyRequest
    {
        [Required] public int VacancyId { get; set; }
        [Required] public string JobSeekerEmail { get; set; }
    }
}