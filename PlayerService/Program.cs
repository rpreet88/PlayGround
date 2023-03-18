
// Add Services --------------------------------------------------------------------------------------

using PlayerService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PlayerStore>();

builder.Services.AddHttpClient("StatsApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://statsapi.web.nhl.com/");
});

builder.Services.AddTransient<IStatsApiClient>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpclient = httpClientFactory.CreateClient("StatsApi");
    if (httpclient is null)
    {
        throw new Exception("Failed to setup StatsApiClient.");
    }

    return new StatsApiClient(httpclient);
});


// Build App -----------------------------------------------------------------------------------------

var app = builder.Build();

// Configure App -------------------------------------------------------------------------------------

var playerStore = app.Services.GetService<PlayerStore>();
if (playerStore is null)
{
    throw new Exception("Failed to initiate player store.");
}
playerStore.InitiatePlayers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
