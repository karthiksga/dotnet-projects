using FastEndpoints;
using FluentValidation;
using VerticalSlicesArchitecture.Extensions;

namespace VerticalSlicesArchitecture;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddFastEndpoints();
        builder.Services.AddSwaggerGen();

        builder.Services.ConfigureDatabase(builder.Configuration);

        var assembly = typeof(Program).Assembly;

        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        builder.Services.AddValidatorsFromAssembly(assembly);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseFastEndpoints();
        app.ApplyMigrations();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}