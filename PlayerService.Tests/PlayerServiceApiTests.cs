using PlayerService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using PactNet;
using PactNet.Verifier;
using PactNet.Infrastructure.Outputters;

namespace PlayerService.Tests;

public class PlayerServiceApiTests : IClassFixture<PlayerServiceFixture>
{
    private readonly PlayerServiceFixture fixture;

    public PlayerServiceApiTests(PlayerServiceFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void PlayerService_DraftSimulator_GetPlayer()
    {
       // Arrange
        string pactPath = Path.Combine("..",
                                       "..",
                                       "..",
                                       "..",
                                       "Pacts",
                                       "DraftSimulator-PlayerService.json");

        var config = new PactVerifierConfig
        {
            LogLevel = PactLogLevel.Debug,
            Outputters = new[] { new ConsoleOutput() }
        };

        // Act / Assert
        Thread.Sleep(25000);
        IPactVerifier pactVerifier = new PactVerifier(config);
        pactVerifier
            .ServiceProvider("PlayerService", fixture.ServerUri)
            .WithFileSource(new FileInfo(pactPath))
            .WithSslVerificationDisabled()
            .Verify();
    }  
}


public class PlayerServiceFixture : IDisposable
{
    private readonly IHost server;

    public Uri ServerUri { get; }

    public PlayerServiceFixture()
    {
        this.ServerUri = new Uri("http://localhost:8081");

        this.server = Host.CreateDefaultBuilder()
                            .ConfigureWebHostDefaults(webBuilder =>
                            {
                                webBuilder.UseUrls(this.ServerUri.ToString());
                                webBuilder.UseStartup<Startup>();
                            })
                            .Build();

        this.server.Start();
    }

    public void Dispose()
    {
        this.server.Dispose();
    }
}
