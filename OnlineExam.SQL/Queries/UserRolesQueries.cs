using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.SQL.Queries;

public static class UserRolesQueries
{
    public static string AllUserRoles => "SELECT * FROM [UserRoles] (NOLOCK)";

    public static string UserRoleById => "SELECT * FROM [UserRoles] (NOLOCK) WHERE [Id] = @Id";
}
