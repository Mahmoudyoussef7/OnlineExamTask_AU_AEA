using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace OnlineExam.Core.Entities;

public class StudentProgress:BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public bool IsCompleted { get; set; }
    public decimal Score { get; set; }
    public DateTime TimeStamp { get; set; }
}
