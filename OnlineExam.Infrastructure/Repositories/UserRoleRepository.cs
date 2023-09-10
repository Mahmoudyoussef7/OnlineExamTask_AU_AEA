using Microsoft.Extensions.Configuration;
using OnlineExam.Core.Entities;
using OnlineExam.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OnlineExam.SQL.Queries;
using OnlineExam.Application.Interfaces;

namespace OnlineExam.Infrastructure.Repositories;

public class UserRoleRepository :IUserRoleRepository
{
    private readonly IConfiguration configuration;

    public UserRoleRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private IDbConnection CreateConnection()
    {
        IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
        connection.Open();
        return connection;
    }

    //public async Task<string> AddAsync(UserRole entity)
    //{
    //    using IDbConnection connection = CreateConnection();
    //    var result = await connection.ExecuteAsync(UserRolesQueries.AddUserRole, entity);
    //    return result.ToString();
    //}

    //public async Task<string> DeleteAsync(int id)
    //{
    //    using IDbConnection connection = CreateConnection();
    //    var result = await connection.ExecuteAsync(UserRolesQueries.DeleteUserRole, new { Id = id });
    //    return result.ToString();
    //}

    public async Task<IReadOnlyList<UserRole>> GetAllAsync()
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QueryAsync<UserRole>(UserRolesQueries.AllUserRoles);
        return result.ToList();

    }

    public async Task<UserRole> GetByIdAsync(int id)
    {
        using IDbConnection connection = CreateConnection();
        var result = await connection.QuerySingleOrDefaultAsync<UserRole>(UserRolesQueries.UserRoleById, new { Id = id });
        return result;
    }

    public Task<string> AddAsync(UserRole entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> UpdateAsync(UserRole entity)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserRole> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<string> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    //public async Task<string> UpdateAsync(UserRole entity)
    //{
    //    using IDbConnection connection = CreateConnection();
    //    var result = await connection.ExecuteAsync(UserRolesQueries.UpdateUserRole, entity);
    //    return result.ToString();
    //}
}
