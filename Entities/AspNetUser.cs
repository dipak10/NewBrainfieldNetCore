using Microsoft.AspNetCore.Identity;
using System;

namespace NewBrainfieldNetCore.Entities
{
    public class AspNetUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
