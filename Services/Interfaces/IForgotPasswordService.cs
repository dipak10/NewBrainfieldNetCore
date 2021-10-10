using NewBrainfieldNetCore.Viewmodels.ForgotPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface IForgotPasswordService
    {
        Task ForgotPassword(ForgotPasswordViewModel viewModel, string link);

        Task<string> GenerateToken(ForgotPasswordViewModel viewModel);
    }
}
