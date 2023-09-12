namespace OnlineExam.SQL.Queries;

public static class UserRolesQueries
{
    public static string AllUserRoles => "SELECT * FROM [UserRoles] (NOLOCK)";

    public static string UserRoleById => "SELECT * FROM [UserRoles] (NOLOCK) WHERE [Id] = @Id";

    public static string AddUserRole =>
        @"INSERT INTO [UserRole] ([Id], [RoleName]) VALUES (@Id, @RoleName)";

    public static string UpdateUserRole =>
        @"UPDATE [UserRole] 
        SET [RoleName] = @RoleName
        WHERE [Id] = @Id";

    public static string DeleteUserRole => "DELETE FROM [UserRole] WHERE [Id] = @Id";
}
