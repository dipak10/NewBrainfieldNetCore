using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public class tblStudyMaterialPayment
    {
		[Key]
		public int StudyMaterialPaymentID { get; set; }
		public string Name { get; set; }
		public string EmailID { get; set; }
		public string PhoneNumber { get; set; }
		public int StudyMaterialFileID { get; set; }
		public string OrderNo { get; set; }
		public bool IsPaid { get; set; }
		public bool IsDownloaded { get; set; }
		public DateTime? DownloadedOn { get; set; }
		public DateTime? CreatedOn { get; set; }
	}
}
