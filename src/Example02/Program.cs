using Example02;
using Example02.Endpoints;
using Example02.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

app.UseOpenApi();

app.UseHttpsRedirection();

app.MapLoggingEndpoints();

await app.RunAsync();