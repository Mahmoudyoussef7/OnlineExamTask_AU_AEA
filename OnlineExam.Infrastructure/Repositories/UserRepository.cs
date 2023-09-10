using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using OnlineExam.SQL.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineExam.Application.Interfaces;

namespace OnlineExam.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly IConfiguration configuration;

    public UserRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(User entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(UserQueries.RegisterUser, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(UserQueries.DeleteUser, new { Id = id });
        return result.ToString();
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<User>(UserQueries.AllUsers);
        return result.ToList();

    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.UserById, new { Id = id });
        return result;
    }
    
    public async Task<User> GetUserByEmail(string email)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.UserByEmail, new { Email = email });
        return result;
    }

    public async Task<string> UpdateAsync(User entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(UserQueries.UpdateUser, entity);
        return result.ToString();
    }
}
