using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using OnlineExam.SQL.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineExam.Application.Interfaces;

namespace OnlineExam.Infrastructure.Repositories;

public class ExaminationRepository :IExaminationRepository
{
    private readonly IConfiguration configuration;

    public ExaminationRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(Examination entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(ExaminationsQueries.AddExamination, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(ExaminationsQueries.DeleteExamination, new { Id = id });
        return result.ToString();
    }

    public async Task<IReadOnlyList<Examination>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<Examination>(ExaminationsQueries.AllExaminations);
        return result.ToList();

    }

    public async Task<Examination> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<Examination>(ExaminationsQueries.ExaminationById, new { Id = id });
        return result;
    }


    public async Task<string> UpdateAsync(Examination entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(ExaminationsQueries.UpdateExamination, entity);
        return result.ToString();
    }
}
