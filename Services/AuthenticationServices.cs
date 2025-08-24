using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstractions;
using Shared.DataTransferDto.IdentityDto;

namespace Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> _userManager ,IConfiguration _configuration ,IMapper _mapper) : IAuthenticationServices
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            return user is not null;
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user =await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };
                
            
        }

        public async Task<AddressesDto> GetCurrnetUserAddressAsync(string email)
        {
            var user = _userManager.Users.Include(u => u.Address)
                .FirstOrDefault(u => u.Email ==  email)?? throw new UserNotFoundException(email);

            if (user.Address is not null)
            {
                return  _mapper.Map<Address, AddressesDto>(user.Address);
            }else
                throw new AddressNotFoundException(user.DisplayName);
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user =await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UserNotFoundException(loginDto.Email);

            var IsPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (IsPassword)
            {
                return new UserDto
                {                   
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token =await CreateTokenAsync(user),
                };
            }
            throw new UnAuthorizeException();


        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result =await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token =await CreateTokenAsync(user),
                };
            }
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new BadRequsteException(errors);
        }

        public async Task<AddressesDto> UpdateCurrnetAddressAsync(string email, AddressesDto addressDto)
        {
            var user = _userManager.Users.Include(u => u.Address)
                .FirstOrDefault(u => u.Email == email) ?? throw new UserNotFoundException(email);
            if(user.Address is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }
            else
            {

               user.Address = _mapper.Map<AddressesDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressesDto>(user.Address);
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user) 
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecretyKey = _configuration.GetSection("JWTOption")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretyKey));
            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWTOption")["Issuer"],
                audience : _configuration.GetSection("JWTOption")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
