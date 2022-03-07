using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Employer.BusinessLogic.Interfaces;
using Employer.Infrastructure.Models;
using Employer.Infrastructure.RequestResponseModels;
using Employer.Infrastructure.RequestResponseModels.Employer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employer.API.Controllers
{
    [ApiController]
    [Route("/api/employer")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IMapper _mapper;

        public EmployerController(IEmployerRepository employerRepository, IMapper mapper)
        {
            _employerRepository = employerRepository;
            _mapper = mapper;
        }

        [HttpGet("get-details/")]
        public async Task<ActionResult<EmployerResponseDto>> GetDetails([FromQuery]string email)
        {
            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("employer with given email not found");
            var obj = _mapper.Map<EmployerResponseDto>(await _employerRepository.GetEmployer(email));
            return obj;
        }

        [HttpGet("get-details/{id:int}")]
        public async Task<ActionResult<EmployerResponseDto>> GetDetails(int id)
        {
            var emp = await _employerRepository.GetEmployerFromId(id);
            if (emp == null) return BadRequest("employer not found");
            return _mapper.Map<EmployerResponseDto>(emp);
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("create-details")]
        public async Task<ActionResult> CreateDetails(DetailsDto details)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //    _employerRepository.DoesEmployerExist(get email from token)
            if (await _employerRepository.DoesEmployerExist(email))
                return BadRequest("Employer already exists");

            var empEntity = _mapper.Map<EmployerEntity>(details);
            empEntity.CreatedByEmailUser = email;
            await _employerRepository.CreateEmployerDetails(empEntity);
            return Ok();
        }

        [Authorize(Roles = "Employer")]
        [HttpPut("update-details")]
        public async Task<ActionResult> UpdateDetails(DetailsDto details)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("Employer Doesn't exist please create your employer profile first");

            var empEntity = await _employerRepository.GetEmployer(email);
            _mapper.Map(details, empEntity);
            await _employerRepository.UpdateEmployerDetail(empEntity);
            return Ok();
        }
    }
}