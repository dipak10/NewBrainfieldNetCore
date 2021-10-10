using NewBrainfieldNetCore.Common;
using NewBrainfieldNetCore.Viewmodels.ResetPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface IResetPasswordService
    {
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel model);
    }
}
