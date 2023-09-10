using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using OnlineExam.SQL.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OnlineExam.UI;
using OnlineExam.Application.Interfaces;

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

    //public async Task<string> AddAsync(QuestionType entity)
    //{
    //    using IDbConnection connection = CreateConnection();
    //    var result = await connection.ExecuteAsync(QuestionTypesQueries.AddQuestionType, entity);
    //    return result.ToString();
    //}

    //public async Task<string> DeleteAsync(int id)
    //{
    //    using IDbConnection connection = CreateConnection();
    //    var result = await connection.ExecuteAsync(QuestionTypesQueries.DeleteQuestionType, new { Id = id });
    //    return result.ToString();
    //}

    public async Task<IReadOnlyList<QuestionType>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<QuestionType>(QuestionTypesQueries.AllQuestionTypes);
        return result.ToList();

    }

    public async Task<QuestionType> GetByIdAsync(int id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<QuestionType>(QuestionTypesQueries.QuestionTypeById, new { Id = id });
        return result;
    }

    public Task<string> AddAsync(QuestionType entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateAsync(QuestionType entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<QuestionType> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    //public async Task<string> UpdateAsync(QuestionType entity)
    //{
    //    using IDbConnection connection = CreateConnection();
    //    var result = await connection.ExecuteAsync(QuestionTypesQueries.UpdateQuestionType, entity);
    //    return result.ToString();
    //}
}
