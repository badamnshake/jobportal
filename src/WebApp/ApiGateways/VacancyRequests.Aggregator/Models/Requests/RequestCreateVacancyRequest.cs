﻿namespace VacancyRequests.Aggregator.Models.Requests
{
    public class RequestCreateVacancyRequest
    {
        public int vacancyId { get; set; }
        public string jobSeekerEmail { get; set; }
    }
}