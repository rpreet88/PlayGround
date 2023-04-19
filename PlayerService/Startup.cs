using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayerService;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<PlayerStore>();

        services.AddHttpClient("StatsApi", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://statsapi.web.nhl.com/");
        });

        services.AddTransient<IStatsApiClient>(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var httpclient = httpClientFactory.CreateClient("StatsApi");
            if (httpclient is null)
            {
                throw new Exception("Failed to setup StatsApiClient.");
            }

            return new StatsApiClient(httpclient);
        });

        services.AddAuthorization();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {   
        var playerStore = app.ApplicationServices.GetService<PlayerStore>();
        if (playerStore is null)
        {
            throw new Exception("Failed to initiate player store.");
        }
        playerStore.InitiatePlayers();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
