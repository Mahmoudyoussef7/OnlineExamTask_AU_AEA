using Microsoft.Extensions.Configuration;
using OnlineExam.Application.Interfaces;
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

namespace OnlineExam.Infrastructure.Repositories;

public class ChoiceRepository:IChoiceRepository
{
    private readonly IConfiguration configuration;

    public ChoiceRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(Choice entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(ChoicesQueries.AddChoice, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(ChoicesQueries.DeleteChoice, new { Id = id });
        return result.ToString();
    }

    public async Task<IReadOnlyList<Choice>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<Choice>(ChoicesQueries.AllChoices);
        return result.ToList();
    }
    
    public async Task<IEnumerable<Choice>> GetChoicesOfQuestion(Guid questionId)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<Choice>(ChoicesQueries.ChoiceByQuestionId,new {Id=questionId});
        return result.ToList();
    }

    public async Task<Choice> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<Choice>(ChoicesQueries.ChoiceById, new { Id = id });
        return result;
    }

    public async Task<string> UpdateAsync(Choice entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(ChoicesQueries.UpdateChoice, entity);
        return result.ToString();
    }
}



