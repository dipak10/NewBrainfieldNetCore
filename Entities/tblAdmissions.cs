using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblAdmissions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int AdmissionId { get; set; }
        public string AdmissionOrderNo { get; set; }
        public string AdmissionTXNID { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string DOB { get; set; }
        public string Nationality { get; set; }
        public string Religion { get; set; }
        public string Category { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string AdharNo { get; set; }
        public string StudentEmail { get; set; }
        public string StudentContactNo { get; set; }
        public string Address { get; set; }
        public string FatherContactNo { get; set; }
        public string StudentImage { get; set; }
        public string SchoolName { get; set; }
        public string AdmissionStd { get; set; }
        public string Board { get; set; }
        public string Session { get; set; }
        public string LastExamName1 { get; set; }
        public string LastBoard1 { get; set; }
        public string LastYear1 { get; set; }
        public string LastPercentage1 { get; set; }
        public string LastExamName2 { get; set; }
        public string LastBoard2 { get; set; }
        public string LastYear2 { get; set; }
        public string LastPercentage2 { get; set; }
        public string LastExamName3 { get; set; }
        public string LastBoard3 { get; set; }
        public string LastYear3 { get; set; }
        public string LastPercentage3 { get; set; }
        public string FatherOccupation { get; set; }
        public string AnnualFamilyIncome { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string IFSCCode { get; set; }
        public string RefName { get; set; }
        public string RefAddress { get; set; }
        public string RefPhone { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool HasMadePayment { get; set; }
    }
}
