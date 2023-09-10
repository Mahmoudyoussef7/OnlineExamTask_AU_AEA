using OnlineExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Interfaces;

public interface IChoiceRepository:IRepository<Choice>
{
    Task<IEnumerable<Choice>> GetChoicesOfQuestion(Guid questionId);
}
