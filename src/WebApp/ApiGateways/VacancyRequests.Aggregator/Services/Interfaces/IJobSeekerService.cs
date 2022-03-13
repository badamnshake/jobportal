using System.Net.Http;
using System.Threading.Tasks;

namespace VacancyRequests.Aggregator.Services.Interfaces
{
    public interface IJobSeekerService
    {
        
        Task<HttpResponseMessage> DeleteJobSeeker(string email);
    }
}