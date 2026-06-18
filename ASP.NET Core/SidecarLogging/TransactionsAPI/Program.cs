using System.Text.Json.Serialization;
using TransactionsAPI.Background;
using TransactionsAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IThreadSafeFileLogger, ThreadSafeFileLogger>();
builder.Services.AddSingleton<ISidecarMessageQueue, SidecarMessageQueue>();
builder.Services.AddHostedService<TransactionsBackgroundService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
