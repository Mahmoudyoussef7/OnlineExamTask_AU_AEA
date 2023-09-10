using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.SQL.Queries;

public static class UserQueries
{
    public static string AllUsers => "SELECT * FROM [Users] (NOLOCK)";

    public static string UserById => "SELECT * FROM [Users] (NOLOCK) WHERE [Id] = @Id";
    public static string UserByEmail => "SELECT * FROM [Users] (NOLOCK) WHERE [Email] = @Email";

    public static string RegisterUser =>
        @"INSERT INTO [Users] ([Id], [UserName], [Email], [Password], [FirstName], [LastName], [RoleId]) 
            VALUES (@Id, @UserName, @Email, @Password,@FirstName,@LastName, @RoleId)";

    public static string UpdateUser =>
        @"UPDATE [Users] 
        SET [UserName] = @UserName, 
            [Email] = @Email, 
            [Password] = @Password, 
            [FirstName] = @FirstName,
            [LastName] = @LastName,
            [RoleId]=@RoleId
        WHERE [Id] = @Id";

    public static string DeleteUser => "DELETE FROM [Users] WHERE [Id] = @Id";

    public static string LoginUser => "SELECT * FROM users WHERE Email = '@Email' AND password = '@Password'";
}
