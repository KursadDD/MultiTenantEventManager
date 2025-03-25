using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantEventManager.Application.Abstractions.Services;
using MultiTenantEventManager.Application.DTOs;
using MultiTenantEventManager.Application.Enums;
using MultiTenantEventManager.Domain.Entities;

namespace MultiTenantEventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, IUserService userService, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.Queryable().FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _authService.GenerateToken(user);

            return Ok(token);
        }

        [Authorize(Roles = "TenantAdmin, SuperAdmin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(createUserDto);
            var result = await _userService.CreateUserAsync(user);

            if(!result.IsSuccess) return BadRequest(result.Message);

            return Ok(result.Message);

        }
    }
}
