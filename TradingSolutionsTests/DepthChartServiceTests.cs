using TradingSolutions.Models;
using TradingSolutions.Services;
using FluentAssertions;

namespace TradingSolutionsTests;

public class DepthChartServiceTests
{
    private readonly IDepthChartService _depthChartService;
    private int asd = 1;

    public DepthChartServiceTests()
    {
        _depthChartService = new DepthChartService();
    }

    [Fact]
    public void AddPlayer_ShouldAddPlayerAtCorrectPositionInNFL()
    {
        // Arrange
        var player = new Player { Number = 12, Name = "Tom Brady", Position = "QB" };

        // Act
        _depthChartService.AddPlayerToDepthChart("QB", player, 0);

        // Assert
        var depthChart = _depthChartService.GetFullDepthChart();
        Assert.Equal(player, depthChart["QB"][0]);
    }

    [Fact]
    public void AddPlayerToDepthChart_ShouldAddPlayerAtPositionDepth_AndShiftOthersDown()
    {
        // Arrange
        var tomBrady = new Player { Number = 12, Name = "Tom Brady", Position = "QB" };
        var kyleTrask = new Player { Number = 2, Name = "Kyle Trask", Position = "QB" };
        var blaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert", Position = "QB" };

        _depthChartService.AddPlayerToDepthChart("QB", tomBrady);
        _depthChartService.AddPlayerToDepthChart("QB", kyleTrask);

        // Act
        _depthChartService.AddPlayerToDepthChart("QB", blaineGabbert, 0);

        // Assert
        var depthChart = _depthChartService.GetFullDepthChart();
        Assert.Equal(blaineGabbert, depthChart["QB"][0]); // Blaine Gabbert should be at the top
        Assert.Equal(tomBrady, depthChart["QB"][1]); // Tom Brady should be shifted to index 1
        Assert.Equal(kyleTrask, depthChart["QB"][2]); // Kyle Trask should be shifted to index 2
    }

    [Fact]
    public void AddPlayerToDepthChart_ShouldThrowArgumentOutOfRangeException_WhenPositionDepthIsInvalid()
    {
        // Arrange
        var tomBrady = new Player { Number = 12, Name = "Tom Brady", Position = "QB" };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _depthChartService.AddPlayerToDepthChart("QB", tomBrady, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => _depthChartService.AddPlayerToDepthChart("QB", tomBrady, 1)); // No players yet, so index 1 is invalid
    }

    [Fact]
    public void RemovePlayerFromDepthChart_ShouldRemovePlayerCorrectly()
    {
        // Arrange
        var player = new Player { Number = 12, Name = "Tom Brady", Position = "QB" };
        _depthChartService.AddPlayerToDepthChart("QB", player);

        // Act
        var removedPlayer = _depthChartService.RemovePlayerFromDepthChart("QB", player);

        // Assert
        Assert.Equal(player, removedPlayer);
        var depthChart = _depthChartService.GetFullDepthChart();
        Assert.DoesNotContain(player, depthChart["QB"]);
    }

    [Fact]
    public void GetBackups_ShouldReturnCorrectBackups()
    {
        // Arrange
        var TomBrady = new Player { Number = 12, Name = "Tom Brady", Position = "QB" };
        var BlaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert", Position = "QB" };
        var KyleTrask = new Player { Number = 2, Name = "Kyle Trask", Position = "QB" };
        _depthChartService.AddPlayerToDepthChart("QB", TomBrady, 0);
        _depthChartService.AddPlayerToDepthChart("QB", BlaineGabbert, 1);
        _depthChartService.AddPlayerToDepthChart("QB", KyleTrask, 2);

        // Act
        var backupsForTomBrady = _depthChartService.GetBackups("QB", TomBrady);
        var backupsForBlaineGabbert = _depthChartService.GetBackups("QB", BlaineGabbert);
        var backupsForKyleTrask = _depthChartService.GetBackups("QB", KyleTrask);

        // Assert
        Assert.Equal(2, backupsForTomBrady.Count);
        Assert.Contains(BlaineGabbert, backupsForTomBrady);
        Assert.Contains(KyleTrask, backupsForTomBrady);

        Assert.Single(backupsForBlaineGabbert);
        Assert.Contains(KyleTrask, backupsForBlaineGabbert);

        Assert.Empty(backupsForKyleTrask);
    }

    [Fact]
    public void GetFullDepthChart_ShouldReturnCompleteDepthChart()
    {
        // Arrange
        var TomBrady = new Player { Number = 12, Name = "Tom Brady", Position = "QB" };
        var BlaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert", Position = "QB" };
        var KyleTrask = new Player { Number = 2, Name = "Kyle Trask", Position = "QB" };

        var MikeEvans = new Player { Number = 13, Name = "Mike Evans", Position = "LWR" };
        var JaelonDarden = new Player { Number = 1, Name = "Jaelon Darden", Position = "LWR" };
        var ScottMiller = new Player { Number = 10, Name = "Scott Miller", Position = "LWR" };

        _depthChartService.AddPlayerToDepthChart("QB", TomBrady, 0);
        _depthChartService.AddPlayerToDepthChart("QB", BlaineGabbert, 1);
        _depthChartService.AddPlayerToDepthChart("QB", KyleTrask, 2);

        _depthChartService.AddPlayerToDepthChart("LWR", MikeEvans, 0);
        _depthChartService.AddPlayerToDepthChart("LWR", JaelonDarden, 1);
        _depthChartService.AddPlayerToDepthChart("LWR", ScottMiller, 2);

        // Act
        var fullDepthChart = _depthChartService.GetFullDepthChart();

        // Assert
        Assert.True(fullDepthChart.ContainsKey("QB"));
        Assert.Equal(3, fullDepthChart["QB"].Count);
        Assert.Equal(TomBrady, fullDepthChart["QB"][0]);
        Assert.Equal(BlaineGabbert, fullDepthChart["QB"][1]);
        Assert.Equal(KyleTrask, fullDepthChart["QB"][2]);

        Assert.True(fullDepthChart.ContainsKey("LWR"));
        Assert.Equal(3, fullDepthChart["LWR"].Count);
        Assert.Equal(MikeEvans, fullDepthChart["LWR"][0]);
        Assert.Equal(JaelonDarden, fullDepthChart["LWR"][1]);
        Assert.Equal(ScottMiller, fullDepthChart["LWR"][2]);
    }

    [Fact]
    public void RemoveNonExistentPlayer_ShouldReturnEmpty()
    {
        // Arrange
        var player = new Player { Number = 99, Name = "Unknown Player", Position = "QB" };
        var expectedResult = new Player();

        // Act
        var result = _depthChartService.RemovePlayerFromDepthChart("QB", player);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GetBackups_ShouldReturnEmptyList_WhenNoBackupsExist()
    {
        // Arrange
        var tomBrady = new Player { Number = 12, Name = "Tom Brady", Position = "QB" };
        _depthChartService.AddPlayerToDepthChart("QB", tomBrady, 0);

        // Act
        var backups = _depthChartService.GetBackups("QB", tomBrady);

        // Assert
        Assert.Empty(backups); // Tom Brady has no backups
    }

    [Fact]
    public void GetBackups_ShouldReturnEmptyList_WhenPlayerNotInDepthChart()
    {
        // Arrange
        var unknownPlayer = new Player { Number = 99, Name = "Unknown Player", Position = "QB" };

        // Act
        var backups = _depthChartService.GetBackups("QB", unknownPlayer);

        // Assert
        Assert.Empty(backups); // Unknown player is not in the depth chart
    }

}