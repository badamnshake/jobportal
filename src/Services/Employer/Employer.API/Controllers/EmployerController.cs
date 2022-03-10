using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Employer.BusinessLogic.Interfaces;
using Employer.Infrastructure.Models;
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

        [HttpGet("get")]
        public async Task<ActionResult<ResponseEmployerDetails>> GetDetails()
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("employer with given email not found");
            var obj = _mapper.Map<ResponseEmployerDetails>(await _employerRepository.GetEmployer(email));
            return obj;
        }
        [HttpGet("get/{email}")]
        public async Task<ActionResult<ResponseEmployerDetails>> GetDetails(string email)
        {
            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("employer with given email not found");
            var obj = _mapper.Map<ResponseEmployerDetails>(await _employerRepository.GetEmployer(email));
            return obj;
        }

        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<ResponseEmployerDetails>> GetDetails(int id)
        {
            if (!await _employerRepository.DoesEmployerExistById(id)) return BadRequest("employer not found");
            var emp = await _employerRepository.GetEmployerFromId(id);
            
            var obj = _mapper.Map<ResponseEmployerDetails>(emp);
            return obj;
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateDetails(RequestEmployerDetails requestEmployerDetails)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //    _employerRepository.DoesEmployerExist(get email from token)
            if (await _employerRepository.DoesEmployerExist(email))
                return BadRequest("Employer already exists");

            var empEntity = _mapper.Map<EmployerEntity>(requestEmployerDetails);
            empEntity.CreatedByEmailUser = email;
            await _employerRepository.CreateEmployerDetails(empEntity);
            return Ok();
        }

        [Authorize(Roles = "Employer")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateDetails(RequestEmployerDetails requestEmployerDetails)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("Employer Doesn't exist please create your employer profile first");

            var empEntity = await _employerRepository.GetEmployer(email);
            _mapper.Map(requestEmployerDetails, empEntity);
            await _employerRepository.UpdateEmployerDetail(empEntity);
            return Ok();
        }
    }
}