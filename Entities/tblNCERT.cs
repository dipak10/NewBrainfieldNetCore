using System;

namespace NewBrainfieldNetCore.Entities
{
    public class tblNCERT
	{
		public int NcertID { get; set; }
		public int NcertCatID { get; set; }
		public int ChapterMasterId { get; set; }
		public string NcertLink { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
