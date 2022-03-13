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

        // gets the details of employer
        [HttpGet("get")]
        public async Task<ActionResult<ResponseEmployerDetails>> GetDetails()
        {
            // for the logged in employer it takes email from the token
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("employer with given email not found");
            var obj = _mapper.Map<ResponseEmployerDetails>(await _employerRepository.GetEmployer(email));
            return obj;
        }

        // gets the employer from email
        // only the request aggregator uses this method otherwise it doesn't make sense to use this
        // request aggregator fetches the details from this
        // because when fetching vacancy requests of job seekers it is necessary to get employer 
        [HttpGet("get/{email}")]
        public async Task<ActionResult<ResponseEmployerDetails>> GetDetails(string email)
        {
            if (!await _employerRepository.DoesEmployerExist(email))
                return BadRequest("employer with given email not found");
            // mapper maps from employer to employer details
            var obj = _mapper.Map<ResponseEmployerDetails>(await _employerRepository.GetEmployer(email));
            return obj;
        }


        // the reason for this endpoint was to get employers for job seekers
        // making it searchable through id
        // as vacancy has employer id in it

        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<ResponseEmployerDetails>> GetDetails(int id)
        {
            if (!await _employerRepository.DoesEmployerExistById(id)) return BadRequest("employer not found");
            var emp = await _employerRepository.GetEmployerFromId(id);

            var obj = _mapper.Map<ResponseEmployerDetails>(emp);
            return obj;
        }

        // it takes identity token where user has logged in as employer
        // and it creates the employer profile
        [Authorize(Roles = "Employer")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateDetails(RequestEmployerDetails requestEmployerDetails)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // if employer details exist then return
            if (await _employerRepository.DoesEmployerExist(email))
                return BadRequest("Employer already exists");

            // it maps from request to employer entity
            var empEntity = _mapper.Map<EmployerEntity>(requestEmployerDetails);

            // email works as identity foreign key for purpose and taken from token
            empEntity.CreatedByEmailUser = email;
            await _employerRepository.CreateEmployerDetails(empEntity);
            return Ok();
        }

        // it takes identity token where user has logged in as employer
        // and it updates the employer profile
        // checks are as 'create' method
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


        // it deletes the employer
        // this route doesn't exist directly
        // only gateway aggregator deletes the employer profile automatically
        // when the Identity is deleted {identity api}
        [HttpDelete("delete/{email}")]
        public async Task<ActionResult> DeleteEmployer(string email)
        {
            if (!await _employerRepository.DoesEmployerExist(email))
                // if employer doesn't exist no need to remove
                return Ok();

            await _employerRepository.DeleteEmployer(email);
            return Ok();
        }
    }
}