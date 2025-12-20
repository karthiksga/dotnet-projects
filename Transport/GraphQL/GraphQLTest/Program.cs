using GraphQLTest.DataRepository;
using GraphQLTest.Extensions;
using GraphQLTest.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

builder.Services.AddDbContext<BlogsContext>(
       (s, opt) =>
           opt.UseSqlite($"Data Source=blogs.sqlite")
           .LogTo(Console.WriteLine, new[]
           {
                DbLoggerCategory.Database.Command.Name
           }));

using (var serviceProvider = builder.Services.BuildServiceProvider())
{
    Seeder.CheckAndSeedAsync(serviceProvider.GetService<BlogsContext>()).Wait();
}


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapGraphQL("/"));

app.Run();
