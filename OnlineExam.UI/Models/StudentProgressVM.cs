namespace OnlineExam.UI.Models
{
    public class StudentProgressVM
    {
        public string UserName { get; set; } = string.Empty;
        public string ExamTitle { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public decimal Score { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
