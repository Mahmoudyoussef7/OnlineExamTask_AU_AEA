namespace OnlineExam.Core.Entities;

public class Examination : BaseEntity
{
    public string ExamTitle { get; set; } = string.Empty;
    public string ExamDescription { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int QuestionCount { get; set; }
    public decimal DurationInHours { get; set; }
}
