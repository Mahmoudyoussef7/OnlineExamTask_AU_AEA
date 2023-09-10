using OnlineExam.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Application.Interfaces;

public interface IAnswerRepository:IRepository<Answer>
{
    Task<Answer> GetAnswerStudentForQuestion(Guid userId, Guid examId, Guid questionId);
    Task<List<Answer>> GetAnswerExamUser(Guid userId, Guid examId);
}
