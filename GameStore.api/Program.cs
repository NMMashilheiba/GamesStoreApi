using GameStore.api.Data;
using GameStore.api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpints();

app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
