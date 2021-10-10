using NewBrainfieldNetCore.Common;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Viewmodels.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface ISignUpService
    {
        Task<SignUpResult> SignUp(RegisterViewModel registerViewModel);

        Task SendEmail(AspNetUser user, string token, string link);

        Task<string> GenerateToken(AspNetUser user);

        Task<bool> ConfirmEmail(string userId, string token);
    }
}
