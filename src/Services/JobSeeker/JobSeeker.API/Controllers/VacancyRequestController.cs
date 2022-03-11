using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.VacancyRequest;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/vacancy-request")]
    public class VacancyRequestController : ControllerBase
    {
        private readonly IVacancyRequestRepository _vacancyRequestRepository;
        private readonly IJobSeekerUserRepository _jobSeekerUserRepository;
        private readonly IMapper _mapper;

        public VacancyRequestController(IVacancyRequestRepository vacancyRequestRepository,
            IJobSeekerUserRepository jobSeekerUserRepository, IMapper mapper)
        {
            _vacancyRequestRepository = vacancyRequestRepository;
            _jobSeekerUserRepository = jobSeekerUserRepository;
            _mapper = mapper;
        }

        [HttpGet("get/{vacancyId:int}")]
        public async Task<List<ResponseJobSeekerUser>> GetVacancyRequestJobSeekers(int vacancyId)
        {
            var jobSeekers = await _vacancyRequestRepository.GetVacancyRequestJobSeekers(vacancyId);
            return _mapper.Map<List<ResponseJobSeekerUser>>(jobSeekers);
        }

        [HttpPost("create/")]
        public async Task<ActionResult> CreateVacancyRequest(RequestCreateVacancyRequest request)
        {
            var email = request.jobSeekerEmail;
            var jobSeeker = await _jobSeekerUserRepository.GetJobSeeker(email);


            if (jobSeeker.Id == 0)
                return BadRequest("you need to create your profile first");

            if (await _vacancyRequestRepository.DoesVacancyRequestExist(request.vacancyId, jobSeeker.Id))
            {
                return BadRequest("You already applied for this vacancy");
            }

            var vacancyRequest = new VacancyRequest
            {
                VacancyId = request.vacancyId,
                JobSeekerUserId = jobSeeker.Id,
                AppliedDate = DateTime.Now
            };

            await _vacancyRequestRepository.CreateVacancyRequest(vacancyRequest);

            return Ok();
        }

        [HttpDelete("delete/{vacancyRequestId:int}")]
        public async Task<ActionResult> DeleteVacancyRequest(int vacancyRequestId)
        {
            var vacReq = await _vacancyRequestRepository.GetVacancyRequestFromId(vacancyRequestId);
            if (vacReq == null) return BadRequest("Vacancy Request doesn't exist");
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (vacReq.JobSeekerUserId != jobSeekerId) return Unauthorized("You can't delete request you don't own");
            await _vacancyRequestRepository.DeleteVacancyRequest(vacReq);
            return Ok();
        }
    }
}