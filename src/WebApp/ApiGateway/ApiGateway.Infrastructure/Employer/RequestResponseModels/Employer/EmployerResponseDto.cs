namespace ApiGateway.Infrastructure.Employer.RequestResponseModels.Employer
{
    public class EmployerResponseDto
    {
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public int NoOfEmployees { get; set; }
        public int StartYear { get; set; }
        public string About { get; set; }
    }
}