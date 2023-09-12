using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using OnlineExam.SQL.Queries;
using System.Data.SqlClient;
using System.Data;
using OnlineExam.Application.Interfaces;
using Dapper;

namespace OnlineExam.Infrastructure.Repositories;

public class QuestionChoicesRepository:IQuestionChoicesRepository
{
    private readonly IConfiguration configuration;

    public QuestionChoicesRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(QuestionChoices entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionChoicesQueries.AddQuestionChoices, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionChoicesQueries.DeleteQuestionChoices, new { Id = id });
        return result.ToString();
    }

    public async Task<IReadOnlyList<QuestionChoices>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<QuestionChoices>(QuestionChoicesQueries.AllQuestionChoices);
        return result.ToList();
    }
    public async Task<IReadOnlyList<Guid>> GetChoiceIdsByQuestionIdAsync(Guid questionId)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<Guid>(QuestionChoicesQueries.ChoicesByQuestionIdAsync, new { QuestionId = questionId});
        return result.ToList();
    }

    public async Task<QuestionChoices> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<QuestionChoices>(QuestionChoicesQueries.QuestionChoicesByIds, new { Id = id });
        return result;
    }

    public async Task<string> UpdateAsync(QuestionChoices entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionChoicesQueries.UpdateQuestionChoices, entity);
        return result.ToString();
    }
}