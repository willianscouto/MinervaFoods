using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MinervaFoods.Helpers.Security;

namespace MinervaFoods.Api.Filters
{
    public class ManagerOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userAuth = context.HttpContext.RequestServices.GetService(typeof(IUserAuthenticate)) as IUserAuthenticate;

           
        }
    }
}