namespace DemoFile.Test.Integration;

[TestFixture]
public class DemoParserIntegrationTest
{
    [Test]
    public async Task ReadAll()
    {
        var demo = new DemoParser();
        await demo.ReadAllAsync(GotvCompetitiveProtocol13963, default);
        Assert.That(demo.CurrentDemoTick.Value, Is.EqualTo(217866));
    }

    [Test]
    public async Task ByTick()
    {
        // Arrange
        var demo = new DemoParser();
        var tick = demo.CurrentDemoTick;

        // Act
        await demo.StartReadingAsync(GotvCompetitiveProtocol13963, default);
        while (await demo.MoveNextAsync(default))
        {
            // Tick is monotonic
            Assert.That(demo.CurrentDemoTick.Value, Is.GreaterThanOrEqualTo(tick.Value));
            tick = demo.CurrentDemoTick;
        }

        // Assert
        Assert.That(demo.CurrentDemoTick.Value, Is.EqualTo(217866));
    }

    private static readonly KeyValuePair<string, Stream>[] CompatibilityCases =
    {
        new("v13978", GotvProtocol13978),
        new("v13980", GotvProtocol13980),
    };

    [TestCaseSource(nameof(CompatibilityCases))]
    public async Task ReadAll_Compatibility(KeyValuePair<string, Stream> testCase)
    {
        var demo = new DemoParser();
        await demo.ReadAllAsync(testCase.Value, default);
    }

    [Test]
    public async Task ReadAll_AlternateBaseline()
    {
        var demo = new DemoParser();
        await demo.ReadAllAsync(MatchmakingProtocol13968, default);
    }

    [Test]
    public void ParseNonAsync()
    {
        var demo = new DemoParser();
        demo.StartNonAsync(GotvCompetitiveProtocol13963);
        while (!demo.ReachedEndOfFile)
            demo.ReadNext();

        Assert.That(demo.CurrentDemoTick.Value, Is.EqualTo(217866));
        Assert.That(demo.CurrentGameTick.Value, Is.EqualTo(337402));
    }

    [Test]
    public void ParseNonAsync_AlternateBaseline()
    {
        var demo = new DemoParser();
        demo.StartNonAsync(MatchmakingProtocol13968);
        while (!demo.ReachedEndOfFile)
            demo.ReadNext();

        Assert.That(demo.CurrentDemoTick.Value, Is.EqualTo(78460));
        Assert.That(demo.CurrentGameTick.Value, Is.EqualTo(86245));
    }
}
