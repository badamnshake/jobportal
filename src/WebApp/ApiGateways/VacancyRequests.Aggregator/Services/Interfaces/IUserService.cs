using System.Net.Http;
using System.Threading.Tasks;

namespace VacancyRequests.Aggregator.Services.Interfaces
{
    public interface IUserService
    {
        Task<HttpResponseMessage> DeleteUser(string email);
    }
}