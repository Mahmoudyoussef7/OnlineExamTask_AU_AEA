using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using OnlineExam.SQL.Queries;
using OnlineExam.Application.Interfaces;

namespace OnlineExam.Infrastructure.Repositories;

public class StudentProgressRepository : IStudentProgressRepository
{
    private readonly IConfiguration configuration;

    public StudentProgressRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    public async Task<string> AddAsync(StudentProgress entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(StudentProgressQueries.AddStudentProgress, entity);
        return result.ToString();
    }

    public async Task<string> DeleteAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(StudentProgressQueries.DeleteStudentProgress, new { Id = id });
        return result.ToString();
    }

    public async Task<IReadOnlyList<StudentProgress>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<StudentProgress>(StudentProgressQueries.AllStudentProgress);
        return result.ToList();
    }
    public async Task<IReadOnlyList<StudentProgress>> GetAllStudentProgressAsync(Guid userId)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<StudentProgress>(StudentProgressQueries.StudentProgressByUserId, new {UserId=userId});
        return result.ToList();
    }

    public async Task<StudentProgress> GetByIdAsync(Guid id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<StudentProgress>(StudentProgressQueries.StudentProgressByUserId, new { Id = id });
        return result;
    }

    public async Task<StudentProgress> GetExamProgressByUserId(Guid userId, Guid examId)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<StudentProgress>(StudentProgressQueries.StudentProgressByUserId, new { ExamId = examId, UserId = userId });
        return result;
    }
    public async Task<string> UpdateAsync(StudentProgress entity)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.ExecuteAsync(StudentProgressQueries.UpdateStudentProgress, entity);
        return result.ToString();
    }
}
