using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Helpers
{
    public static class ConvertDateTime
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public static DateTime ConvertToIndianTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        }


        //public static SubjectDTO ToDo(this List<tblSubject> tblSubject, SubjectDTO subjectDTO)
        //{
        //    subjectDTO.SubjectID = tblSub ject.SubjectID;
        //    subjectDTO.SubjectName = tblSubject.SubjectName;
        //    subjectDTO._Standards = tblSubject.Standards;
        //    return subjectDTO;
        //}
    }
}
