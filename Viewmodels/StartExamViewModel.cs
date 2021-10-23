using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels
{
    [Keyless]
    public class StartExamViewModel
    {
        public IEnumerable<StartExamViewModel> _Results { get; set; }

        public List<Tuple<int, string>> SubjectMasters { get; set; }

        public string CandidateName { get; set; }

        public string ExamName { get; set; }

        public StartExamViewModel(IEnumerable<StartExamViewModel> results, List<Tuple<int, string>> subjectMasters, string candidateName, string examName, int examId, int userId)
        {
            _Results = results;
            SubjectMasters = subjectMasters;
            CandidateName = candidateName;
            ExamName = examName;
            ExamID = examId;
            UserID = userId;
        }

        public StartExamViewModel()
        {

        }
        public int UserID { get; set; }
        public int ExamQuestionID { get; set; }
        public Nullable<int> ExamID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; }
        public string Question { get; set; }
        public int Mark { get; set; }
        public decimal NegativeMark { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
    }
}
