using Microsoft.Extensions.Configuration;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineExam.SQL.Queries;

namespace OnlineExam.Infrastructure.Repositories;

public class AnswerRepository : IAnswerRepository
{
    private readonly IConfiguration configuration;

    public AnswerRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(Answer entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(AnswersQueries.AddAnswer, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(AnswersQueries.DeleteAnswer, new { Id = id });
        return result.ToString();
    }

    public async Task<IReadOnlyList<Answer>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<Answer>(AnswersQueries.AllAnswers);
        return result.ToList();
    }

    public async Task<Answer> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<Answer>(AnswersQueries.AnswerById, new { Id = id });
        return result;
    }

    public async Task<Answer> GetAnswerStudentForQuestion(Guid questionId, Guid userId, Guid examId)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<Answer>(AnswersQueries.GetAnswerStudentForQuestion, new { QuestionId = questionId, UserId = userId, ExamId = examId });
        return result;
    }

    public async Task<string> UpdateAsync(Answer entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(AnswersQueries.UpdateAnswer, entity);
        return result.ToString();
    }
}
