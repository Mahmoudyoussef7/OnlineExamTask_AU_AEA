using OnlineExam.Core.Entities;

namespace OnlineExam.Application.Interfaces;

public interface IQuestionChoicesRepository : IRepository<QuestionChoices>
{
    Task<IReadOnlyList<Guid>> GetChoiceIdsByQuestionIdAsync(Guid questionId);
}
