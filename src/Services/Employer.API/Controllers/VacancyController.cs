using System;
using System.Threading.Tasks;
using AutoMapper;
using Employer.API.DTOs;
using Employer.API.Entities;
using Employer.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employer.API.Controllers
{
    [ApiController]
    [Route("/api/vacancy")]
    public class VancancyController : ControllerBase
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IMapper _mapper;
        public VancancyController(IVacancyRepository vacancyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _vacancyRepository = vacancyRepository;
        }
        [HttpGet("get-details")]
        public async Task<ActionResult<Vacancy>> GetDetails([FromQuery]int id)
        {
            Vacancy vacancy = await _vacancyRepository.GetVacancyDetails(id);
            if(vacancy == null)
            return BadRequest();
            return vacancy;
        }
        [HttpPost("create-details")]
        public async Task<ActionResult<VacancyReponseDetailsDto>> CreateDetails(VacancyDetailsDto details)
        {
            Vacancy vac = _mapper.Map<Vacancy>(details);
            vac.PublishedDate = DateTime.Now;
            var vacancy = await _vacancyRepository.AddVacancy(vac);
            return _mapper.Map<VacancyReponseDetailsDto>(vacancy);

        }
        [HttpPut("update-details")]
        public async Task<ActionResult> UpdateDetails(VacancyUpdateDto details)
        {
            Vacancy vacancy = await _vacancyRepository.GetVacancyDetails(details.Id);
            if (vacancy == null)
            {
                return BadRequest(" the vancancy doesn't exist");
            }
            await _vacancyRepository.UpdateVacancy(vacancy);
            return Ok();
        }
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteVacancy(VacancyDeleteDto details)
        {
            Vacancy vacancy = await _vacancyRepository.GetVacancyDetails(details.Id);
            if (vacancy == null)
            {
                return BadRequest(" the vancancy doesn't exist");
            }
            await _vacancyRepository.DeleteVacancy(vacancy);
            return Ok();
        }
    }
}