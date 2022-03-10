using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/vacancy-request")]
    public class VacancyRequestController : ControllerBase
    {
        private readonly IVacancyRequestRepository _vacancyRequestRepository;
        private readonly IMapper _mapper;

        public VacancyRequestController(IVacancyRequestRepository vacancyRequestRepository,
            IJobSeekerUserRepository jobSeekerUserRepository, IMapper mapper)
        {
            _vacancyRequestRepository = vacancyRequestRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Employer")]
        [HttpGet("get/{vacancyId:int}")]
        public async Task<List<ResponseJobSeekerUser>> GetVacancyRequestJobSeekers(int vacancyId)
        {
            var jobSeekers = await _vacancyRequestRepository.GetVacancyRequestJobSeekers(vacancyId);
            return _mapper.Map<List<ResponseJobSeekerUser>>(jobSeekers);
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateVacancyRequest([FromQuery] int vacancyId)
        {
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            if (jobSeekerId == 0)
                return Forbid("you need to create your profile first");
            
            if (await _vacancyRequestRepository.DoesVacancyRequestExist(vacancyId, jobSeekerId))
            {
                return BadRequest("You already applied for this vacancy");
            }

            var vacancyRequest = new VacancyRequest
            {
                VacancyId = vacancyId,
                JobSeekerUserId = jobSeekerId,
                AppliedDate = DateTime.Now
            };

            await _vacancyRequestRepository.CreateVacancyRequest(vacancyRequest);

            return Ok();
        }

        [Authorize(Roles = "JobSeeker")]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteVacancyRequest([FromQuery] int vacancyRequestId)
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