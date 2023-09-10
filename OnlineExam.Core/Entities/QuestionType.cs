using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Core.Entities;

public class QuestionType
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string TypeName { get; set; } = string.Empty;
}
