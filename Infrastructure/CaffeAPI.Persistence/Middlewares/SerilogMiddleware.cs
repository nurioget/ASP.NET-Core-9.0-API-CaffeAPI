using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Persistence.Middlewares
{
    public class SerilogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SerilogMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var request = context.Request;
            var ip = context.Connection.RemoteIpAddress?.ToString();
            //var username = context.User.Identity?.Name ?? "Anoim";
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("_e");
            var username = claim != null ? claim.Value : "Anonim";
            var requestPath = request.Path;

            using (LogContext.PushProperty("Username", username))
            using (LogContext.PushProperty("Requestpath", requestPath))
            using (LogContext.PushProperty("RequestMethod", request.Method))
            using (LogContext.PushProperty("RequestIp", ip))
            {
                Log.Information("Request: {Method} {Path} from {IP}", request.Method, request.Path, ip);

                try
                {
                    await _next(context);
                    sw.Stop();
                    Log.Information("Response: {StatusCode} in {Elapsed} ms", context.Response.StatusCode, sw.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    sw.Stop();
                    Log.Error(ex, "Error Occurred Duration: {Elapsed} ms", sw.ElapsedMilliseconds);
                    throw;
                }
            }
        }
    }
}
