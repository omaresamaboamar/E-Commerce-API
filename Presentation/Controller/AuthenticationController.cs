using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferDto.IdentityDto;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _serviceManager) : ControllerBase
    {
       // [Authorize]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.AuthenticationServices.LoginAsync(loginDto);
            return Ok(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        }
        // Check Email
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var user = await _serviceManager.AuthenticationServices.CheckEmailAsync(email);
            return Ok(user);
        }
        // Get Current user
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user =await _serviceManager.AuthenticationServices.GetCurrentUserAsync(Email);
            return Ok(user);
        }
        // Get Current User Address
        [HttpGet("Address")]
        public async Task<ActionResult<AddressesDto>> GetCurrentUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationServices.GetCurrnetUserAddressAsync(Email);
            return Ok(address);
        }
        // Update Current User Address
        [HttpPut("Address")]
        public async Task<ActionResult<AddressesDto>> UpdateCurrentUserAddress(AddressesDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationServices.UpdateCurrnetAddressAsync(email, addressDto);
            return Ok(address);
        }
    }
}
