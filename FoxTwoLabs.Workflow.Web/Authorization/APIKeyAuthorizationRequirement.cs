using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FoxTwoLabs.Workflow.Web.Authorization
{
    public class APIKeyAuthorizationRequirement : IAuthorizationRequirement
    {
        public string APIKey { get; set; }

        public APIKeyAuthorizationRequirement(string apiKey)
        {
            APIKey = apiKey;
        }
    }

    public class APIKeyRequirementHandler: AuthorizationHandler<APIKeyAuthorizationRequirement>
    {

        public const string API_KEY_HEADER_NAME = "api_key_header";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, APIKeyAuthorizationRequirement requirement)
        {
            if (context.Resource is AuthorizationFilterContext authorizationFilterContext)
            {
                var apiKey = authorizationFilterContext.HttpContext.Request.Headers[API_KEY_HEADER_NAME].FirstOrDefault();
                if (apiKey == requirement.APIKey)
                {
                    context.Succeed(requirement);
                }

            }
            return Task.CompletedTask;
        }

    }
}
