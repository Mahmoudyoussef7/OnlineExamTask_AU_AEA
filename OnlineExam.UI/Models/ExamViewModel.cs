using OnlineExam.UI.Custom;
using System.ComponentModel.DataAnnotations;

namespace OnlineExam.UI.Models;

public class ExamViewModel
{
    [MaxLength(100)]
    [Display(Name = "Title")]
    public string ExamTitle { get; set; } = string.Empty;

    [Display(Name = "Description")]
    public string ExamDescription { get; set; } = string.Empty;

    [DateRange("StartDate", "EndDate", ErrorMessage = "End date must be greater than or equal to the start date.")]
    [Display(Name = "Start at")]
    public DateTime StartDate { get; set; }

    [Display(Name = "End at")]
    public DateTime EndDate { get; set; }

    [Display(Name = "Number of Questions")]
    public int QuestionCount { get; set; }

    [Display(Name = "Duration (Hours)")]
    public decimal DurationInHours { get; set; }
}
