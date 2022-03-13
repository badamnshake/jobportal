using System.Net.Http;
using System.Threading.Tasks;

namespace VacancyRequests.Aggregator.Services.Interfaces
{
    public interface IEmployerService
    {

        public Task<HttpResponseMessage> GetDetails(string email);
        public Task<HttpResponseMessage> DeleteEmployer(string email);
    }
}