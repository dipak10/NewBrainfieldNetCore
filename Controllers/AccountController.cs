using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewBrainfieldNetCore.Common;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels.ForgotPassword;
using NewBrainfieldNetCore.Viewmodels.Login;
using NewBrainfieldNetCore.Viewmodels.ResetPassword;
using NewBrainfieldNetCore.Viewmodels.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ISignUpService _signUpService;
        private readonly ILoginService _loginService;
        private readonly IForgotPasswordService _forgotPasswordService;
        private readonly IResetPasswordService _resetPasswordService;

        private readonly IEmailSender _mailService;

        public AccountController(ILogger<AccountController> logger,
            ISignUpService signUpService, IEmailSender mailService,
            ILoginService loginService,
            IForgotPasswordService forgotPasswordService,
            IResetPasswordService resetPasswordService)
        {
            _logger = logger;
            _signUpService = signUpService;
            _mailService = mailService;
            _loginService = loginService;
            _forgotPasswordService = forgotPasswordService;
            _resetPasswordService = resetPasswordService;
        }

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var login = await _loginService.Login(model);
                if (login.IsSuccess)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (login.IsLockOut)
                {
                    //var forgotPassLink = Url.Action("ForgotPassword", "Account", new { }, Request.Scheme);
                    //var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);

                    //await _loginService.SendEmail(model.EmailAddress, content);
                }

                if (login.IdPasswordWrong)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }
            return View();
        }

        #endregion Login

        #region Register

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            _logger.LogDebug($"Click on register {registerViewModel}");

            if (ModelState.IsValid)
            {
                try
                {
                    SignUpResult signUpResult = await _signUpService.SignUp(registerViewModel);
                    if (signUpResult.IsCreatedSuccessful && signUpResult.EmailSent == false)
                    {
                        var token = await _signUpService.GenerateToken(signUpResult.User);
                        if (!string.IsNullOrEmpty(token))
                        {
                            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                new { userId = signUpResult.User.Id, token = token }, Request.Scheme);


                            await _signUpService.SendEmail(signUpResult.User, token, confirmationLink);
                        }



                        _logger.Log(LogLevel.Information, "Created Successfully");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($" Error Occured while Creating User {e.InnerException}");
                }
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Error", "Account");
            }

            bool verify = await _signUpService.ConfirmEmail(userId, token);

            if (verify)
            {
                return RedirectToAction("Success", "Account");
            }

            return RedirectToAction("Error", "Account");
        }

        public async Task<IActionResult> Success()
        {
            return View();
        }

        public async Task<IActionResult> Error()
        {
            return View();
        }

        #endregion Register

        #region ForgotPasswordandReset

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string token = await _forgotPasswordService.GenerateToken(model);

                if (!string.IsNullOrEmpty(token))
                {                   
                    var callbackUrl = Url.Action("ResetPassword", "Account",
                                new { email = model.Email, token = token }, protocol: Request.Scheme);

                    await _forgotPasswordService.ForgotPassword(model, callbackUrl);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ResetPasswordResult result = await _resetPasswordService.ResetPassword(model);
                if (result.IsSuccess && result.IsUserFound)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                if (result.IsSuccess == false && result.Errors.Count > 0)
                {
                    foreach (var er in result.Errors)
                    {
                        ModelState.AddModelError(er.Key, er.Value);
                    }
                }
            }
            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion ForgotPasswordandReset

    }
}
