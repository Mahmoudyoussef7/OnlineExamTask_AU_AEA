using OnlineExam.Core.Entities;

namespace OnlineExam.Application.Interfaces;

public interface IExaminationRepository:IRepository<Examination>
{
    Task<IReadOnlyList<Examination>> GetExaminationsByUserId(Guid id);
}
