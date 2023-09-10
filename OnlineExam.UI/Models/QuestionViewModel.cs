using OnlineExam.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineExam.UI.Models;

public class QuestionViewModel
{
    public Guid ExamId { get; set; }

    [MinLength(20)]
    public string QuestionText { get; set; } = string.Empty;

    public int QuestionType { get; set; }

    public List<Choice> Questions { get; set; } = new List<Choice>();
}
