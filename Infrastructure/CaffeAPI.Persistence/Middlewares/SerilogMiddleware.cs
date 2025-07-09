using Microsoft.AspNetCore.Http;
using Serilog;
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
        public SerilogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var request = context.Request;
            var ip = context.Connection.RemoteIpAddress?.ToString();
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
