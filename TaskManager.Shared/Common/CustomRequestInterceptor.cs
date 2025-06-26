using System.Security.Claims;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Shared.Common;

public class CustomRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override async ValueTask OnCreateAsync(HttpContext httpContext, IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        var user = httpContext.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = user.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                requestBuilder.SetGlobalState("userId", Guid.Parse(userId));
            }

            if (!string.IsNullOrEmpty(role))
            {
                requestBuilder.SetGlobalState("userRole", role);
            }
        }

        await base.OnCreateAsync(httpContext, requestExecutor, requestBuilder, cancellationToken);
    }
}