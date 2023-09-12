using OnlineExam.Core.Entities;

namespace OnlineExam.Application.Interfaces;

public interface IQuestionRepository : IRepository<Question>
{
    Task<IEnumerable<Question>> GetByExamIdAsync(Guid id);
}
