using Microsoft.AspNetCore.Identity;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IEmailSender _emailService;

        public LoginService(UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager, IEmailSender emailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailService = emailService;
        }


        public async Task<NewBrainfieldNetCore.Common.SignInResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(
                    loginViewModel.EmailAddress, loginViewModel.Password, false, true);

            if (result.Succeeded)
            {
                return new Common.SignInResult
                {
                    IsSuccess = true,
                    Data = result
                };
            }

            if (result.IsLockedOut)
            {
                return new Common.SignInResult
                {
                    IsLockOut = true
                };
            }

            return new Common.SignInResult
            {
                IdPasswordWrong = true
            };
        }

        public async Task SendEmail(string email, string content)
        {
            var request = new MailRequest("dipak10494@yahoo.com", "Brainfield Forgotpassword Link", content);
            await _emailService.SendEmailAsync(request);
        }
    }
}
