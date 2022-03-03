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

        [HttpGet("get-details")]
        public async Task<ActionResult<EmployerResponseDto>> GetDetails(UserEmailDto userEmailDto)
        {
            var email = userEmailDto.Email;
            if (await _employerRepository.DoesEmployerExist(email))
            {
                var obj = _mapper.Map<EmployerResponseDto>(await _employerRepository.GetEmployer(email));
                return obj;
            }

            return BadRequest();
        }

        [Authorize(Roles = "Employer")]
        [HttpPost("create-details")]
        public async Task<ActionResult> CreateDetails(DetailsDto details)
        {
            //    _employerRepository.DoesEmployerExist(get email from token)
            if (await _employerRepository.DoesEmployerExist(details.CreatedByEmailUser))
                return BadRequest("Employer already exists");
            var empEntity = _mapper.Map<EmployerEntity>(details);
            // string email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // empEntity.CreatedByEmailUser = email;
            await _employerRepository.CreateEmployerDetails(empEntity);
            return Ok();
        }

        [Authorize(Roles = "Employer")]
        [HttpPut("update-details")]
        public async Task<ActionResult> UpdateDetails(DetailsDto details)
        {
            //    _employerRepository.DoesEmployerExist(get email from token)
            var empEntity = await _employerRepository.GetEmployer(details.CreatedByEmailUser);
            _mapper.Map<DetailsDto, EmployerEntity>(details, empEntity);
            await _employerRepository.UpdateEmployerDetail(empEntity);
            return Ok();
        }
    }
}