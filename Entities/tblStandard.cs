using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public class tblStandard
    {
        [Key]
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }
}
