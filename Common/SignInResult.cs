using System.Collections.Generic;

namespace NewBrainfieldNetCore.Common
{
    public class SignInResult
    {
        public bool IsLockOut { get; set; } = false;

        public bool IdPasswordWrong { get; set; } = false;

        public bool EmailNotVerified { get; set; } = false;

        public bool IsSuccess { get; set; } = false;

        public dynamic Data { get; set; } = false;
    }

    public class ResetPasswordResult
    {
        public bool IsSuccess { get; set; } = false;

        public bool IsUserFound { get; set; } = true;

        public Dictionary<string,string> Errors { get; set; }
    }
}
