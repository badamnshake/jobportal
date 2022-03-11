﻿using System.Net.Http;
using System.Threading.Tasks;
using VacancyRequests.Aggregator.Models.Requests;

namespace VacancyRequests.Aggregator.Services.Interfaces
{
    public interface IVacancyRequestService
    {
        public Task<HttpResponseMessage> CreateVacancyRequest(int vacancyId, VacancyRequestModel vacancyRequest);
        public Task<HttpResponseMessage> GetAppliedJobSeekersOnAVacancy(int vacancyId);

    }
}