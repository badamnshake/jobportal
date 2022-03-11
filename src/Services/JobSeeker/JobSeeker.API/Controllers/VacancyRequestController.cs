using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.Models;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.VacancyRequest;
using JobSeeker.Infrastrucure.RequestResponseModels.ResponseModels;
using Microsoft.AspNetCore.Authorization;

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

        // this path will be accessed by api gateway aggregator
        // it is not exposed directly through ocelot
        // managed by vacancy request aggregator
        // that is why no auth is added because aggregator checks auth
        [HttpPost("create/")]
        public async Task<ActionResult> CreateVacancyRequest(RequestCreateVacancyRequest request)
        {
            // finds the job seeker from email given by agg. (from token received in aggregator)
            var email = request.JobSeekerEmail;
            var jobSeeker = await _jobSeekerUserRepository.GetJobSeeker(email);


            // if job seeker isn't found then they need to create a profile
            if (jobSeeker.Id == 0)
                return BadRequest("you need to create your profile first");

            // if vac req already exists ....
            if (await _vacancyRequestRepository.DoesVacancyRequestExist(request.VacancyId, jobSeeker.Id))
            {
                return BadRequest("You already applied for this vacancy");
            }

            var vacancyRequest = new VacancyRequest
            {
                VacancyId = request.VacancyId,
                JobSeekerUserId = jobSeeker.Id,
                AppliedDate = DateTime.Now
            };

            await _vacancyRequestRepository.CreateVacancyRequest(vacancyRequest);

            return Ok();
        }

        
        
        // it is exposed in ocelot means user works directly with it no aggregator is used
        [Authorize(Roles = "JobSeeker")]
        [HttpDelete("delete/{vacancyRequestId:int}")]
        public async Task<ActionResult> DeleteVacancyRequest(int vacancyRequestId)
        {
            // gets the vacancy request
            var vacReq = await _vacancyRequestRepository.GetVacancyRequestFromId(vacancyRequestId);
            // if not found then return
            if (vacReq == null) return BadRequest("Vacancy Request doesn't exist");
            
            // middlewares gets js from token
            var jobSeekerId = (int) HttpContext.Items["jobSeekerId"]!;
            
            // edge case: if a job seeker doesn't own a vac req then can't delete it 
            if (vacReq.JobSeekerUserId != jobSeekerId) return Unauthorized("You can't delete request you don't own");
            
            await _vacancyRequestRepository.DeleteVacancyRequest(vacReq);
            return Ok();
        }
    }
}