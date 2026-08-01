using KASHOP.BLL.Common;
using KASHOP.DAL.Data;
using KASHOP.DAL.Dto;
using KASHOP.DAL.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IEmailSender _emailsender;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IEmailSender emailSender)
        {
            _usermanager = userManager;
            _emailsender = emailSender;
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();
            var result = await _usermanager.CreateAsync(user, request.Password);//here u must pass th epassword  to hash it

            if (!result.Succeeded)
            {
                return new RegisterResponse
                {
                    Message = "Error",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            var token = await _usermanager.GenerateEmailConfirmationTokenAsync(user);
            token = Uri.EscapeDataString(token);

            var link = $"https://localhost:7233/api/Account/ConfirmEmail?token={token}&userId={user.Id}";
            await _emailsender.SendEmailAsync(request.Email, "Confirm Email","<div><h1>Welcome</h1>" +
                $"<a href ='{link}'>confirm</a>" +
                "</div>");

             return new RegisterResponse { Message = "Success !" }; 
        }
        public async Task<bool> ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await _usermanager.FindByIdAsync(request.UserId);
            if (user == null)  return false;

            request.Token = Uri.UnescapeDataString(request.Token); 

            var result = await _usermanager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)  return false;
            return true;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _usermanager.FindByEmailAsync(request.Email);//returns information about the user
            if (user == null)
            {
                return 
                    new LoginResponse()
                    {
                        Message = "Invalid Email"
                    };
                 }
            if(!await _usermanager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse()
                {
                    Message = "Email is Not Confirmed"
                };
            }
            var resullt = await _usermanager.CheckPasswordAsync(user , request.Password);
            if (!resullt)
            {
                return new LoginResponse() { Message = "Invalid Password" };
            }

            return new LoginResponse() { Message = "Success" };
        }

        
    }
}
