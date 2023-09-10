using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.Entities;

public class Examination : BaseEntity
{
    [MaxLength(100)]
    public string ExamTitle { get; set; } = string.Empty;

    [MinLength(8)]
    public string ExamDescription { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal DurationInHours { get; set; }
}
