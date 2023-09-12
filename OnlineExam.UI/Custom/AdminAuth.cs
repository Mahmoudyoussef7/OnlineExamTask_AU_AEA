using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExam.UI.Custom
{
    public class AdminAuth : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookies = context.HttpContext.Request.Cookies;

            if (cookies.ContainsKey("UserId") && cookies.ContainsKey("RoleId"))
            {
                if (cookies["RoleId"] != "1")
                    context.Result = new ForbidResult();
            }
        }
    }
}
