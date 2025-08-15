using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;
    public AuthorizeRoleAttribute(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userRole = context.HttpContext.Session.GetString("Role");
        if (userRole != _role)
        {
            context.Result = new ForbidResult();
        }
    }
}