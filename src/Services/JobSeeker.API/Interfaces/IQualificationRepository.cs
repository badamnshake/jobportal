
using System.Collections.Generic;
using System.Threading.Tasks;
using JobSeeker.API.DTOs;
using JobSeeker.API.Models;

namespace JobSeeker.API.Interfaces
{
    public interface IQualificationRepository
    {
        Task AddQualification(ReqAddQualification request);
        Task DeleteQualification(ReqDelQualification request);
        Task<IEnumerable<Qualification>> GetQualificationsOfJobSeeker(RequestJobSeekerId request);
    }
}