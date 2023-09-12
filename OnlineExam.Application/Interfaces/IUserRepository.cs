using OnlineExam.Core.Entities;

namespace OnlineExam.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmail(string email);
}
