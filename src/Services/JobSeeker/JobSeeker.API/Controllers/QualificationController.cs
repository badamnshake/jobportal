using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using JobSeeker.BusinessLogic.Interfaces;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels;
using JobSeeker.Infrastrucure.RequestResponseModels.RequestModels.Qualification;

namespace JobSeeker.API.Controllers
{
    [ApiController]
    [Route("/api/qualification")]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationRepository _qualificationRepository;
        private readonly IMapper _mapper;

        public QualificationController(IQualificationRepository qualificationRepository, IMapper mapper)
        {
            _qualificationRepository = qualificationRepository;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<List<ResQualification>> GetQualificationsOfUser(RequestJobSeekerId request)
        {
            var qualification = await _qualificationRepository.GetQualificationsOfJobSeeker(request);
            return _mapper.Map<List<ResQualification>>(qualification);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddQualification(ReqAddQualification request)
        {
            await _qualificationRepository.CreateQualification(request);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteQualification(ReqDelQualification request)
        {
            await _qualificationRepository.DeleteQualification(request);
            return Ok();
        }
    }
}