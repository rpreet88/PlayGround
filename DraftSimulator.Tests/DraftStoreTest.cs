using Moq;
using Microsoft.Extensions.Logging;
using DraftSimulator;

namespace DraftSimulator.Tests;

public class DraftStoreTest
{
    Mock<ILogger<DraftStore>>? mockLogger;

    public DraftStoreTest()
    {
        mockLogger = new Mock<ILogger<DraftStore>>();
    }

    [Fact]
    public void DraftCreatedSuccesfully()
    {
        // Arrange
        NewDraft newDraft = new NewDraft()
        {
            Name = "MockDraft",
            Type = "Hockey",
            NumPlayers = 2,
            Teams = new string[] { "Ryan", "Daniel" }
        };

        #nullable disable
        DraftStore draftStore = new DraftStore(mockLogger.Object);
        Assert.NotNull(draftStore);
        #nullable enable

        // Act
        Draft mockDraft = draftStore.Add(newDraft);

        // Assert
        Assert.NotNull(mockDraft);
        Assert.True(mockDraft.Name == newDraft.Name);
        Assert.True(mockDraft.Type == newDraft.Type);
        Assert.True(mockDraft.NumPlayers == newDraft.NumPlayers);
        foreach(var team in mockDraft.Teams)
        {
            Assert.Contains(team.Name, newDraft.Teams);
        }
    }

    [Fact]
    public void GetDraftSuccesfully()
    {
        // Arrange
        NewDraft newDraft = new NewDraft()
        {
            Name = "MockDraft",
            Type = "Hockey",
            NumPlayers = 2,
            Teams = new string[] { "Ryan", "Daniel" }
        };

        #nullable disable
        DraftStore draftStore = new DraftStore(mockLogger.Object);
        Assert.NotNull(draftStore);
        #nullable enable

        // Act
        Draft mockDraft = draftStore.Add(newDraft);
        Assert.NotNull(mockDraft);
        Draft mockDraftGet = draftStore.Get(mockDraft.DraftId);

        // Assert
        Assert.True(mockDraft == mockDraftGet);
    }

    [Fact]
    public void GetDraftFailure()
    {
        // Arrange
        NewDraft newDraft = new NewDraft()
        {
            Name = "MockDraft",
            Type = "Hockey",
            NumPlayers = 2,
            Teams = new string[] { "Ryan", "Daniel" }
        };
        
        #nullable disable
        DraftStore draftStore = new DraftStore(mockLogger.Object);
        Assert.NotNull(draftStore);
        #nullable enable

        // Act
        Draft mockDraft = draftStore.Add(newDraft);
        Assert.NotNull(mockDraft);

        // Assert
        Assert.Throws<DraftNotFound>(() => draftStore.Get(Guid.NewGuid()));
    }
}