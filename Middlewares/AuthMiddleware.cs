namespace sew.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(context.Request.Path.Value.Contains("/api/auth"))
        {
            await _next.Invoke(context);
        }

        if(context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader)) 
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization header missing");
            return;
        }

        var token = authHeader.FirstOrDefault()?.Split(" ").Last();
        if(string.IsNullOrWhiteSpace(token) || !ValidateJwtToken(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid or missing token");
            return;
        }

        await _next(context);
    }

    private bool ValidateJwtToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var expiration = jwtToken.ValidTo;
            if(expiration < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
        catch(Exception)
        {
            return false;
        }
    }
}
