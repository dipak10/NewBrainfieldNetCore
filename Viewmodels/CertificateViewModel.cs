using System;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class CertificateViewModel
    {
        public string StudentName { get; set; }
        public string ExamName { get; set; }
        public decimal ExamMark { get; set; }
        public Guid UniqueNumber { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
