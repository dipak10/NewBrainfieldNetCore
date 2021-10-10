using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels.ForgotPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IEmailSender _emailService;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ForgotPasswordService(UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager, IEmailSender emailService, IWebHostEnvironment hostingEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailService = emailService;
            this._hostingEnvironment = hostingEnvironment;
        }

        public async Task ForgotPassword(ForgotPasswordViewModel viewModel, string link)
        {
            var request = new MailRequest("dipak10494@yahoo.com", "Brainfield Forgotpassword Link", link);

            await _emailService.SendEmailAsync(request);
        }

        public async Task<string> GenerateToken(ForgotPasswordViewModel viewModel)
        {
            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            // If the user is found AND Email is confirmed
            if (user != null && await _userManager.IsEmailConfirmedAsync(user))
            {
                // Generate the reset password token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return token;
            }
            return string.Empty;

        }
    }
}
