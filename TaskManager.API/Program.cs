using TaskManager.Application;
using TaskManager.GraphQL;
using TaskManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddGraphQLServices();
builder.Services.AddAuthentication("sd"); // JWT
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();
app.Run();