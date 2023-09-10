using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.Entities;

public class Question : BaseEntity
{
    public Guid ExamId { get; set; }

    [MinLength(20)]
    public string QuestionText { get; set; } = string.Empty;

    public int QuestionTypeId { get; set; }
}
