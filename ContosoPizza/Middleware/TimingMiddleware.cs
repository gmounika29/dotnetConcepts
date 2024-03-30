namespace ContosoPizza.Middleware
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TimingMiddleware> _logger;

        public TimingMiddleware(ILogger<TimingMiddleware> logger,RequestDelegate next)
        {
            _logger = logger;  
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            var start= DateTime.UtcNow;
            await _next.Invoke(ctx);
            _logger.LogInformation($"Timing: {ctx.Request.Path}  {(DateTime.UtcNow-start).TotalSeconds}s");
        }
    }
}
