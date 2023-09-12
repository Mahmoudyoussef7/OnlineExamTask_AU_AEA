namespace OnlineExam.Core.Entities;

public class StudentProgress:BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public decimal Score { get; set; }
    public DateTime Timestamp { get; set; }
}
