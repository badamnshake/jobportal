using System.Collections.Generic;

namespace VacancyRequests.Aggregator.Models.Responses
{
    public class ResponseEmployerDetails
    {
        
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public int NoOfEmployees { get; set; }
        public int StartYear { get; set; }
        public string About { get; set; }
        public List<ResponseVacancyDetails> Vacancies { get; set; }
    }
}