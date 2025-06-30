using GameStore.api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpints();

app.Run();
