using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using OnlineExam.SQL.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineExam.Application.Interfaces;

namespace OnlineExam.Infrastructure.Repositories;

public class QuestionRepository:IQuestionRepository
{
    private readonly IConfiguration configuration;

    public QuestionRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(Question entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionsQueries.AddQuestion, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(int id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionsQueries.DeleteQuestion, new { Id = id });
        return result.ToString();
    }

    public async Task<IReadOnlyList<Question>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<Question>(QuestionsQueries.AllQuestions);
        return result.ToList();

    }

    public async Task<Question> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<Question>(QuestionsQueries.QuestionById, new { Id = id });
        return result;
    }
    
    public async Task<IEnumerable<Question>> GetByExamIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<Question>(QuestionsQueries.QuestionByExamId, new { Id = id });
        return result;
    }

    public async Task<string> UpdateAsync(Question entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionsQueries.UpdateQuestion, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionsQueries.DeleteQuestion, new {Id=id});
        return result.ToString();
    }
}
