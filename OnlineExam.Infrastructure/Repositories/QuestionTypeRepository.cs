using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using OnlineExam.SQL.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineExam.Application.Interfaces;
using static Dapper.SqlMapper;

namespace OnlineExam.Infrastructure.Repositories;

public class QuestionTypeRepository : IQuestionTypeRepository
{
    private readonly IConfiguration configuration;

    public QuestionTypeRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(QuestionType entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionTypesQueries.AddQuestionType, entity);
        return result.ToString();
    }

    public async Task<IReadOnlyList<QuestionType>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<QuestionType>(QuestionTypesQueries.AllQuestionTypes);
        return result.ToList();

    }

    public async Task<QuestionType> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<QuestionType>(QuestionTypesQueries.QuestionTypeById, new { Id = id });
        return result;
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionTypesQueries.DeleteQuestionType, new { Id = id });
        return result.ToString();
    }

    public async Task<string> UpdateAsync(QuestionType entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(QuestionTypesQueries.UpdateQuestionType, entity);
        return result.ToString();
    }
}
