using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.DTOs;
using MultiTenantEventManager.Domain.Entities;
using MultiTenantEventManager.Persistence.Services;

namespace MultiTenantEventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;
        public TenantsController(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("current")]
        public async Task<IActionResult> GetTenant()
        {

            var result = await _tenantService.GetAllTenantAsync();
            if (result.Count() == 0) return Ok("Kayıt bulunamadı");

            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantDto createTenantDto)
        {
            var tenant = _mapper.Map<Tenant>(createTenantDto);
            var result =  await _tenantService.CreateTenantAsync(tenant);

            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
