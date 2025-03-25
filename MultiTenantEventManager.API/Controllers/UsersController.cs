using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.DTOs;
using MultiTenantEventManager.Domain.Entities;

namespace MultiTenantEventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper = null)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var tenantId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
            if (tenantId == null)
            {
                return BadRequest("Yetkisiz istek");
            }


            var result = await _userService.Queryable().Where(u => u.TenantId == Convert.ToInt32(tenantId)).ToListAsync();

            if (result.Count() == 0) return Ok("Kayıt bulunamadı");

            return Ok(result);
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetMeUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId == null)
            {
                return BadRequest("Yetkisiz istek");
            }

            var result = await _userService.GetByIdAsync(Convert.ToInt32(userId));
            if (result == null) return Ok("Kayıt bulunamadı");

            return Ok(result);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMeUser([FromBody] UpdateUserDto updateUserDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userId == null) return BadRequest("Yetkisiz istek");

            var entity = await _userService.GetByIdAsync(Convert.ToInt32(userId));
            if (entity == null) return BadRequest("Kayıt bulunamadı");

            _mapper.Map(updateUserDto, entity);
            var result = await _userService.UpdateAsync(entity);

            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}
