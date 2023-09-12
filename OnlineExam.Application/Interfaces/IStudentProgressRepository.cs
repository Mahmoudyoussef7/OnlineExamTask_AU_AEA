using OnlineExam.Core.Entities;

namespace OnlineExam.Application.Interfaces;

public interface IStudentProgressRepository : IRepository<StudentProgress>
{
    Task<StudentProgress> GetExamProgressByUserId(Guid userId, Guid examId);
    Task<IReadOnlyList<StudentProgress>> GetAllStudentProgressAsync(Guid userId);
}
