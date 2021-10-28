using System.Collections.Generic;

namespace NewBrainfieldNetCore.Common
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }

    }
}
