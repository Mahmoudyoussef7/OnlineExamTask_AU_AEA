using System.ComponentModel.DataAnnotations;

namespace OnlineExam.UI.Models
{
    public class StudentProgressVM
    {
        public Guid ExamId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string ExamTitle { get; set; } = string.Empty;
        public decimal StudentScore { get; set; }
        public decimal TotalScore { get; set; }
        [Display(Name ="Exam submission time")]
        public DateTime Timestamp { get; set; }
    }
}
