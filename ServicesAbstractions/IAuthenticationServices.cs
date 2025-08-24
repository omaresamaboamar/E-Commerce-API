using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferDto.IdentityDto;

namespace ServicesAbstractions
{
    public interface IAuthenticationServices
    {
        // Login Endpoint
        // Take Email and Password Then Return Token ,  Email and DisplayName To Client  
        Task<UserDto> LoginAsync(LoginDto loginDto);
        // Register
        // Take Email , Password  , UserName , Display Name And Phone Number
        // Then Return Token , Email and Display Name To Client  
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        // Check Email 
        // Take Email Then Return boolean To Client  
        Task<bool> CheckEmailAsync(string email);

        // Get Current User Address 
        // Take Email Then Return Address of Current Logged in User To Client  
        Task<AddressesDto> GetCurrnetUserAddressAsync(string email);

        // Update Current User Address 
        // Take Updated Address and Email Then Return Address after Update 
        Task<AddressesDto> UpdateCurrnetAddressAsync( string email, AddressesDto addressDto);

        // Get Current User 
        // Take Email Then Return Token , Email and Display Name 
        Task<UserDto> GetCurrentUserAsync(string email);

    }
}
