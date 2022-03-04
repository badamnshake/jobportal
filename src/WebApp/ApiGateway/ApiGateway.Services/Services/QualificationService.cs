using System.Net.Http;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Services.Services
{
    public class QualificationService: IQualificationService
    {
        private readonly HttpClient _httpClient;
        public QualificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }
        
    }
}