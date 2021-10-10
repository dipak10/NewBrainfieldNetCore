using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
