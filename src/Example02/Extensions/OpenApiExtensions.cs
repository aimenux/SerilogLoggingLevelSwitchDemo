using Scalar.AspNetCore;

namespace Example02.Extensions;

public static class OpenApiExtensions
{
    public static void AddOpenApi(this WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsDevelopment())
        {
            return;
            
        }
        
        builder.Services.AddOpenApi();
    }

    public static void UseOpenApi(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.MapOpenApi();
        app.MapScalarApiReference();
    }
}