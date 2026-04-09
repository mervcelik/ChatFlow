using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _httpExceptionHandler = new HttpExceptionHandler();
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context.Response, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpResponse response, Exception ex)
    {
        response.ContentType = "application/problem+json";
        _httpExceptionHandler.Response = response;
        await _httpExceptionHandler.HandleExceptionAsync(ex);
    }
}