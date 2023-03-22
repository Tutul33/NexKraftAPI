using API.Settings;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
namespace API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class Authorizations : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerInfo = context.ActionDescriptor as ControllerActionDescriptor;

            var authorizedToken = context.HttpContext.Request.Headers[""].ToString();
            var userAgent = context.HttpContext.Request.Headers[""].ToString();
            string originAccess = string.Concat(authorizedToken.TakeWhile((c) => c != '-'));
            var userId = context.HttpContext.Request.Headers[""].ToString();
            var platformId = context.HttpContext.Request.Headers[""].ToString();
        }
    }
}
