using NewBrainfieldNetCore.Common;
using NewBrainfieldNetCore.Viewmodels.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface ILoginService
    {
        Task<SignInResult> Login(LoginViewModel loginViewModel);

        Task SendEmail(string email, string content);


    }
}
