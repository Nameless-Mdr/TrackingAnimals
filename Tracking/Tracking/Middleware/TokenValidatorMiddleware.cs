using BLL.Service.Interfaces;

namespace Tracking.Middleware;

public class TokenValidatorMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidatorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISessionService sessionService)
    {
        var isOk = true;

        var sessionString = context.User.Claims.FirstOrDefault(x => x.Type == "sessionId")?.Value;

        if (Guid.TryParse(sessionString, out var sessionId))
        {
            var session = await sessionService.GetSessionById(sessionId);

            if (!session.IsActive)
            {
                isOk = false;
                context.Response.Clear();
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("session is not active");
            }
        }

        if (isOk)
        {
            await _next(context);
        }

        #region TODO
        /*var isOk = true;
        var sessionId = context.User.GetClaimValue<Guid>(ClaimNames.SessionId);
        if (sessionId != default)
        {
            var session = await authService.GetSessionById(sessionId);
            if (!session.IsActive)
            {
                isOk = false;
                context.Response.Clear();
                context.Response.StatusCode = 401;
            }
        }
        if (isOk)
        {
            await _next(context);
        }*/
        #endregion
    }
}

public static class TokenValidatorMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenValidator(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenValidatorMiddleware>();
    }
}