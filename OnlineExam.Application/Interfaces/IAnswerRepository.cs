using OnlineExam.Core.Entities;

namespace OnlineExam.Application.Interfaces;

public interface IAnswerRepository:IRepository<Answer>
{
    Task<Answer> GetAnswerStudentForQuestion(Guid questionId, Guid userId, Guid examId);
}
