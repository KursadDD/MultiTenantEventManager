using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.DTOs;
using MultiTenantEventManager.Domain.Entities;
using MultiTenantEventManager.Domain.Enums;
using MultiTenantEventManager.Persistence.Services;
using System.Diagnostics.Eventing.Reader;

namespace MultiTenantEventManager.API.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Authorize]

    public class EventEntitiesController : ControllerBase
    {
        private readonly IEventEntityService _eventEntityService;
        private readonly IRegistrationService _registrationService;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        public EventEntitiesController(IEventEntityService eventEntityService, IMapper mapper, IRegistrationService registrationService, IMailService mailService)
        {
            _eventEntityService = eventEntityService;
            _mapper = mapper;
            _registrationService = registrationService;
            _mailService = mailService;
        }


        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] EventQueryDto query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eventEntityService.GetFilteredEventsAsync(query);

            return Ok(result);
        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eventEntityService.GetByIdAsync(id);
            if (result == null) return Ok("Kayıt bulunamadı");

            return Ok(result);
        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventEntity = _mapper.Map<EventEntity>(createEventDto);
            var result = await _eventEntityService.CreateAsync(eventEntity);

            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);

        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto updateEventDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _eventEntityService.GetByIdAsync(id);
            if (entity == null) return BadRequest("Kayıt bulunamadı");

            _mapper.Map(updateEventDto, entity);
            var result = await _eventEntityService.UpdateAsync(entity);

            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _eventEntityService.Queryable().Include(I => I.Registrations).FirstOrDefaultAsync(f => f.Id == id);
            if (entity == null) return BadRequest("Kayıt bulunamadı");

            if (entity.Registrations.Any())
            {
                return BadRequest("Bu etkinlik, bağlı kayıtlar olduğu için silinemez.");
            }

            var result = await _eventEntityService.DeleteAsync(id);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [HttpGet("{eventId}/registrations")]
        public async Task<IActionResult> GetRegistrations(int eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eventEntityService.Queryable().Where(e => e.Id == eventId)
                                                              .Include(I => I.Registrations)
                                                              .ToListAsync();
            if (result.Count == 0) return Ok("Kayıt bulunamadı");

            return Ok(result);
        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [HttpPost("{eventId}/registrations")]
        public async Task<IActionResult> CreateRegistration(int eventId, [FromBody] CreateRegistrationDto createRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = _mapper.Map<Registration>(createRegistrationDto);
            registration.EventId = eventId;
            var result = await _registrationService.CreateAsync(registration);

            if (result.IsSuccess && string.IsNullOrEmpty(createRegistrationDto.Email))
            {
                await _mailService.SendMail(createRegistrationDto.Email);
            }

            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);

        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [HttpPut("{eventId}/registrations/{id}")]
        public async Task<IActionResult> UpdateRegistration(int eventId, int id, [FromBody] UpdateRegistrationDto updateRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = await _registrationService.GetByIdAsync(id);
            if (registration == null) return BadRequest("Kayıt bulunamadı");
            if (registration.EventId != eventId) return BadRequest("Kayıt farklı bir etkinliğe ait");

            registration.Status = updateRegistrationDto.Status;
            var result = await _registrationService.UpdateAsync(registration);

            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin, EventManager")]
        [HttpDelete("{eventId}/registrations/{id}")]
        public async Task<IActionResult> DeleteRegistration(int eventId, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = await _registrationService.GetByIdAsync(id);
            if (registration == null) return BadRequest("Kayıt bulunamadı");

            if (registration.EventId != eventId) BadRequest("Kayıt farklı bir etkinliğe ait");

            registration.Status = RegistrationStatusType.Cancelled;
            var result = await _registrationService.UpdateAsync(registration);

            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
