using Microsoft.AspNetCore.Identity;
using NewBrainfieldNetCore.Common;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels.ResetPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IEmailSender _emailService;

        public ResetPasswordService(UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager, IEmailSender emailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailService = emailService;
        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel model)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {                
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return new ResetPasswordResult
                    {
                        IsSuccess = true,
                        IsUserFound = true
                    };
                }
              
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Code, error.Description);
                }

                return new ResetPasswordResult
                {
                    IsSuccess = false,
                    Errors = errors,
                    IsUserFound = true
                };
            }

            return new ResetPasswordResult
            {
                IsSuccess = false,
                IsUserFound = false
            };
        }
    }
}