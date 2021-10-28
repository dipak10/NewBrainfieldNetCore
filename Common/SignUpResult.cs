using NewBrainfieldNetCore.Entities;
using System.Collections.Generic;

namespace NewBrainfieldNetCore.Common
{
    public class SignUpResult
    {
        public bool IsCreatedSuccessful { get; set; }
        public bool EmailSent { get; set; }
        public bool IsUserExist { get; set; }
        public AspNetUser User { get; set; }
        Dictionary<string, string> Data { get; set; }
    }
}
