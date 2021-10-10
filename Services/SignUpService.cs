using Microsoft.AspNetCore.Identity;
using NewBrainfieldNetCore.Common;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NewBrainfieldNetCore.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private IEmailSender _emailService;

        public SignUpService(UserManager<AspNetUser> userManager,
            SignInManager<AspNetUser> signInManager, IEmailSender emailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailService = emailService;
        }

        public async Task<string> GenerateToken(AspNetUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }


        public async Task SendEmail(AspNetUser user, string token, string link)
        {
            var request = new MailRequest("dipak10494@yahoo.com", "Brainfield Confirm Link", link);
            await _emailService.SendEmailAsync(request);            
        }

        public async Task<SignUpResult> SignUp(RegisterViewModel request)
        {
            var userCheck = await _userManager.FindByEmailAsync(request.EmailAddress);
            if (userCheck == null)
            {
                var user = CreateRequest(request);
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    return new SignUpResult
                    {
                        EmailSent = false,
                        IsUserExist = false,
                        IsCreatedSuccessful = true,
                        User = user
                    };
                }
                else
                {
                    Dictionary<string, string> errors = new Dictionary<string, string>();
                    if (result.Errors.Count() > 0)
                    {
                        foreach (var error in result.Errors)
                        {
                            errors.Add("error", error.Description);
                        }
                    }
                    return new SignUpResult
                    {
                        EmailSent = false,
                        IsUserExist = false,
                        IsCreatedSuccessful = false
                    };
                }
            }
            else
            {
                return new SignUpResult
                {
                    EmailSent = false,
                    IsUserExist = true,
                    IsCreatedSuccessful = false
                };
            }
        }

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return false;
            }

            if (user.EmailConfirmed)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return true;
            }
            
            return false;
        }

        private AspNetUser CreateRequest(RegisterViewModel request)
        {
            return new AspNetUser
            {
                FullName = request.StudentName.ToLower(),
                UserName = request.EmailAddress,
                NormalizedUserName = request.EmailAddress,
                Email = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                CreatedOn = System.DateTime.Now.ConvertToIndianTime()
            };
        }
    }
}
