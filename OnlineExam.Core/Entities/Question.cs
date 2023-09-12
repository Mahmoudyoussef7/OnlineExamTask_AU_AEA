namespace OnlineExam.Core.Entities;

public class Question : BaseEntity
{
    public Guid ExamId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int QuestionTypeId { get; set; }
    public string CorrectAnswer { get; set; } = string.Empty;
    public int Points { get; set; }
}
