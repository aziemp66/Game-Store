
using GameStore.API.Data;
using GameStore.API.Routers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRepositories(builder.Configuration);

var app = builder.Build();

app.Services.InitializeDb();

app.MapGet("/", () => "Hello World!");

app.MapGamesRouter();

app.Run();
