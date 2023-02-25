using Moq;
using Microsoft.Extensions.Logging;
using DraftSimulator;

namespace DraftSimulator.Tests;

public class DraftStoreTest
{
    Mock<ILogger<DraftStore>>? mockLogger;

    [Fact]
    public void Test1()
    {
        mockLogger = new Mock<ILogger<DraftStore>>();
        Assert.NotNull(mockLogger);

        DraftStore draftStore = new DraftStore(mockLogger.Object);
        Assert.NotNull(draftStore);
    }
}