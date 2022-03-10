using System.Net.Http;
using System.Threading.Tasks;

namespace VacancyRequests.Aggregator.Services.Interfaces
{
    public interface IVacancyRequestService
    {
        public Task<HttpResponseMessage> CreateVacancyRequest(int vacancyId);
        public Task<HttpResponseMessage> GetAppliedJobSeekersOnAVacancy(int vacancyId);

    }
}