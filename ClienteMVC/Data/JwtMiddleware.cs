namespace ClienteMVC.Data
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
            }

            await _next(context);
        }
    }
    //public class TokenManager
    //{
    //    private static string _token;

    //    public static string Token
    //    {
    //        get => _token;
    //        set => _token = value;
    //    }
    //}

}
