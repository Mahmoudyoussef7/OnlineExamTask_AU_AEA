using OnlineExam.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineExam.UI.Models;

public class ExamViewModel
{
    [MaxLength(100)]
    public string ExamTitle { get; set; } = string.Empty;

    [MinLength(8)]
    public string ExamDescription { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal DurationInHours { get; set; }
    public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
}
