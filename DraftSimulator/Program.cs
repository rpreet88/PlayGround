
using DraftSimulator;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

// Add Services --------------------------------------------------------------------------------------


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<DraftStore>();
builder.Services.AddSingleton<ExceptionHandlerMiddleware>();

builder.Services.AddHttpClient("PlayerClient", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://localhost:8081/");
});

builder.Services.AddTransient<IPlayerClient>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var httpclient = httpClientFactory.CreateClient("PlayerClient");
    if (httpclient is null)
    {
        throw new Exception("Failed to setup StatsApiClient.");
    }

    return new PlayerClient(httpclient);
});

builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<ApiDbContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DraftSimDbConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build App -----------------------------------------------------------------------------------------

var app = builder.Build();

// Configure App -------------------------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
