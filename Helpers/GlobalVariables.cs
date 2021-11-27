using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Helpers
{
    public static class GlobalVariables
    {
        public static int ExamId = 0;
        public static int UserId;
        public static long OrderId = 0;
        public static string ReturnUrl = string.Empty;
        public static int ProductId;
        public static string ControllerName = string.Empty;
        public static string ActionName = string.Empty;
        public static decimal GrandTotal = 0;
        public static decimal Result = 0;
        public static int AccessLevel = 0;
        public static string CurrencyName = string.Empty;
        public static string CountryName = string.Empty;
        public static string ProductType = string.Empty;
        public static int StudyMaterialId = 0;
        public static PaymentRequestFrom PaymentRequestFrom = PaymentRequestFrom.None;

        public static string[] StaticRightWrong = new string[180];

        public static string result = string.Empty;

        public static readonly string Success = "Successfully Added";
        public static readonly string Warning = "Something Went Wrong, Try Again Later.";

        public static readonly string EmptyCart = "Your Cart Is Empty";
    }

    public enum PaymentRequestFrom
    {
        ExamPackage = 0,
        StudyMaterial = 1,
        Videos = 2,
        Admission = 3,
        None = 99
    }
}
