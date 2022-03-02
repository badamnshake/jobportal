using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Experience;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/experience")]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly IMapper _mapper;

        public ExperienceController(IExperienceRepository experienceRepository, IMapper mapper)
        {
            _experienceRepository = experienceRepository;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<List<ResExperience>> GetQualificationsOfUser(RequestJobSeekerId request)
        {
            var qualifictionas = await _experienceRepository.GetExperiencesOfJobSeeker(request);
            return _mapper.Map<List<ResExperience>>(qualifictionas);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddQualification(ReqAddExp request)
        {
            await _experienceRepository.AddExperience(request);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteQualification(ReqDelExp request)
        {
            await _experienceRepository.DeleteExperience(request);
            return Ok();
        }
    }
}