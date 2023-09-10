using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.Entities;

public class Answer : BaseEntity
{
    public Guid UserId { get; set; } 
    public Guid ExamId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid? SelectedChoiceId { get; set; }
    public string? AnswerText { get; set; }
}
