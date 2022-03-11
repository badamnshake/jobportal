using System;

namespace Employer.Infrastructure.Helpers
{
    public class VacancyParams
    {
        // if page size is more than 50 then 
        // page size will be set to 50
        private const int MaxPageSize = 50;
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string Location { get; set; } = null;
        public int MinSalary { get; set; } = 0;
        public int MaxSalary { get; set; } = 0;
        public DateTime LastDateToApply { get; set; } = default;
        public DateTime PublishedDate { get; set; } = default;
        public ToOrderBy OrderBy { get; set; } = ToOrderBy.MinSalaryAscending;
    }

    public enum ToOrderBy
    {
        MinSalaryAscending,
        MinSalaryDescending,
        MaxSalaryDescending,
        LastDateToApply,
        PublishedDate
    }
}