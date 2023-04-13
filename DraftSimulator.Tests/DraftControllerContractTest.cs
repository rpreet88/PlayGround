using System;
using System.Net.Http;
using PactNet;
using Xunit;
using DraftSimulator;

namespace DraftSimulator.Tests;

public class DraftControllerContractTest
{
    private readonly IPactBuilderV3 _pactBuilder;
    private readonly int _port = 9000;

    public DraftControllerContractTest()
    {
        // or specify custom log and pact directories
        var pact = Pact.V3("DraftSimulator", "PlayerService", new PactConfig
        {
            PactDir = $"../../../../Pacts",
            LogLevel = PactLogLevel.Debug
        });

        // Initialize backend
        _pactBuilder = pact.WithHttpInteractions(_port);
    }

    [Fact]
    public async void GetPlayer_WhenCalled_ReturnsPlayer()
    {
        // Define the expected request and response for the Player service
        _pactBuilder
            .UponReceiving("a request to retrieve a player")
                .Given("a player with ID 8473541 exists")
                .WithRequest(HttpMethod.Get, "/player/8473541")
            .WillRespond()
                .WithStatus(System.Net.HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithJsonBody(new 
                {
                    id = 8473541,
                    fullName = "Jonathan Bernier",
                    primaryNumber = "45",
                    currentAge = 34,
                    shootsCatches = "L",
                    currentTeam = new {
                        id = 1,
                        name = "New Jersey Devils",
                        link = "/api/v1/teams/1",
                        locationName = ""
                    },
                    primaryPosition = new {
                        code = "G",
                        name = "Goalie",
                        type = "Goalie"
                    }
                });
        
        // Start the mock server and run the test
        await _pactBuilder.VerifyAsync(async ctx =>
        {
            //Act
            var client = new HttpClient { BaseAddress = ctx.MockServerUri };
            var playerClient = new PlayerClient(client);
            var playerId = 8473541;
            Player player = await playerClient.GetPlayer(playerId).ConfigureAwait(false);

            //Assert
            Assert.Equal("Jonathan Bernier", player.FullName);
            Assert.Equal("45", player.PrimaryNumber);
            Assert.Equal(34, player.CurrentAge);
            Assert.Equal("L", player.ShootsCatches);

            Assert.NotNull(player.CurrentTeam);
            Assert.Equal("New Jersey Devils", player.CurrentTeam.Name);
            Assert.Equal(1, player.CurrentTeam.Id);

            Assert.NotNull(player.PrimaryPosition);
            Assert.Equal("Goalie", player.PrimaryPosition.Type);
        });
    }

}