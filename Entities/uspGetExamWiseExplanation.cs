using Microsoft.EntityFrameworkCore;

namespace NewBrainfieldNetCore.Entities
{
    [Keyless]
    public class uspGetExamWiseExplanation
    {
        public string CorrectAnswer { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public int SubjectID { get; set; }
    }
}
