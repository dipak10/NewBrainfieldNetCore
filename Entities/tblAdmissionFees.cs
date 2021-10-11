using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblAdmissionFees
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeesId { get; set; }       
        public int StandardID { get; set; }        
        public decimal Amount { get; set; }        
        public decimal AppOnlyAmount { get; set; }        
    }
}
