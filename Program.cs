using AutoTrackMinimalApi.Domain.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (LoginDto loginDto) =>
{
    if (loginDto.Email != "admin@teste.com" && loginDto.Senha != "123456")
    {
        return Results.Unauthorized();
    }
    return Results.Ok("Login efetuado com sucesso");
});

app.Run();

