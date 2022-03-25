using System.Net.Http;
using System.Threading.Tasks;

namespace VacancyRequests.Aggregator.Services.Interfaces
{
    public interface IVacancyService
    {
        Task<HttpResponseMessage> GetVacancy(int vacancyId);
        Task<HttpResponseMessage> DeleteVacancy(int vacancyId);
    }
}