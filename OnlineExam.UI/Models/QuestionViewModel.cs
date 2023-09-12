using OnlineExam.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineExam.UI.Models;

public class QuestionViewModel
{
    public Guid ExamId { get; set; }

    [MinLength(20)]
    [Display(Name = "Question text")]
    public string QuestionText { get; set; } = string.Empty;

    [Display(Name = "Type")]
    public int QuestionType { get; set; }

    [Display(Name = "Answer")]
    public string CorrectAnswer { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
    [Display(Name = "Points")]
    public int Points { get; set; }

    public List<Choice> Choices { get; set; } = new List<Choice>();
}
