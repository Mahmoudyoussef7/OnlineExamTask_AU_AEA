using OnlineExam.Core.Entities;

namespace OnlineExam.UI.Models;

public class QuestionAnswerViewModel
{
    public Question Question { get; set; } = new Question();
    public Answer Answer { get; set; } = new Answer();
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public int Count { get; set; }
    public Guid ExamId { get; set; }

    public IEnumerable<Choice> Choices { get; set; } = new List<Choice>();

}
