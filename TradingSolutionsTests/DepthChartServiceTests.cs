using FluentAssertions;
using TradingSolutionsCore.Common;
using TradingSolutionsCore.Models;
using TradingSolutionsCore.Repositories;
using TradingSolutionsCore.Services;

namespace TradingSolutionsTests;

public class DepthChartServiceTests
{
    private readonly IDepthChartService _depthChartService;

    public DepthChartServiceTests()
    {
        var sport = new Sport("NFL");
        sport.AddTeam(AppConstants.Teams.TampaBayBuccaneers);
        var repository = new DepthChartRepository(sport);
        _depthChartService = new DepthChartService(repository);
    }

    [Fact]
    public void AddPlayer_ShouldAddPlayerAtCorrectPositionInNFL()
    {
        // Arrange
        var player = new Player { Number = 12, Name = "Tom Brady"};

        // Act
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", player, 0);

        // Assert
        var depthChart = _depthChartService.GetFullDepthChart("Tampa Bay Buccaneers");
        Assert.Equal(player, depthChart["QB"][0]);
    }

    [Fact]
    public void GetBackups_ShouldReturnCorrectBackups_WhenPlayerHasBackups()
    {
        // Arrange
        var tomBrady = new Player { Number = 12, Name = "Tom Brady" };
        var blaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert" };
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", tomBrady, 0);
        _depthChartService.AddPlayer("Tampa Bay Buccaneers", "QB", blaineGabbert, 1);

        // Act
        var backups = _depthChartService.GetBackups(AppConstants.Teams.TampaBayBuccaneers, "QB", tomBrady);

        // Assert
        Assert.Single(backups);
        Assert.Contains(blaineGabbert, backups);
    }

    [Fact]
    public void RemovePlayer_ShouldRemovePlayerCorrectly()
    {
        // Arrange
        var player = new Player { Number = 12, Name = "Tom Brady" };
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", player, 0);

        // Act
        var removedPlayer = _depthChartService.RemovePlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", player);

        // Assert
        Assert.Equal(player, removedPlayer);
        var depthChart = _depthChartService.GetFullDepthChart(AppConstants.Teams.TampaBayBuccaneers);
        Assert.Empty(depthChart["QB"]);
    }

    [Fact]
    public void AddPlayerToDepthChart_ShouldAddPlayerAtPositionDepth_AndShiftOthersDown()
    {
        // Arrange
        var tomBrady = new Player { Number = 12, Name = "Tom Brady" };
        var kyleTrask = new Player { Number = 2, Name = "Kyle Trask" };
        var blaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert" };

        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", tomBrady, 0);
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", kyleTrask, 1);

        // Act
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", blaineGabbert, 0);

        // Assert
        var depthChart = _depthChartService.GetFullDepthChart(AppConstants.Teams.TampaBayBuccaneers);
        Assert.Equal(blaineGabbert, depthChart["QB"][0]); // Blaine Gabbert should be at the top
        Assert.Equal(tomBrady, depthChart["QB"][1]); // Tom Brady should be shifted to index 1
        Assert.Equal(kyleTrask, depthChart["QB"][2]); // Kyle Trask should be shifted to index 2
    }

    [Fact]
    public void AddPlayerToDepthChart_ShouldThrowArgumentOutOfRangeException_WhenPositionDepthIsInvalid()
    {
        // Arrange
        var tomBrady = new Player { Number = 12, Name = "Tom Brady" };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", tomBrady, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", tomBrady, 1)); // No players yet, so index 1 is invalid
    }


    [Fact]
    public void GetFullDepthChart_ShouldReturnCompleteDepthChart()
    {
        // Arrange
        var TomBrady = new Player { Number = 12, Name = "Tom Brady" };
        var BlaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert" };
        var KyleTrask = new Player { Number = 2, Name = "Kyle Trask" };

        var MikeEvans = new Player { Number = 13, Name = "Mike Evans"};
        var JaelonDarden = new Player { Number = 1, Name = "Jaelon Darden" };
        var ScottMiller = new Player { Number = 10, Name = "Scott Miller" };

        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", TomBrady, 0);
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", BlaineGabbert, 1);
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", KyleTrask, 2);

        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "LWR", MikeEvans, 0);
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "LWR", JaelonDarden, 1);
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "LWR", ScottMiller, 2);

        // Act
        var fullDepthChart = _depthChartService.GetFullDepthChart(AppConstants.Teams.TampaBayBuccaneers);

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
        var player = new Player { Number = 99, Name = "Unknown Player" };
        var expectedResult = new Player();

        // Act
        var result = _depthChartService.RemovePlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", player);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GetBackups_ShouldReturnEmptyList_WhenNoBackupsExist()
    {
        // Arrange
        var tomBrady = new Player { Number = 12, Name = "Tom Brady" };
        _depthChartService.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, "QB", tomBrady, 0);

        // Act
        var backups = _depthChartService.GetBackups(AppConstants.Teams.TampaBayBuccaneers, "QB", tomBrady);

        // Assert
        Assert.Empty(backups); // Tom Brady has no backups
    }

    [Fact]
    public void GetBackups_ShouldReturnEmptyList_WhenPlayerNotInDepthChart()
    {
        // Arrange
        var unknownPlayer = new Player { Number = 99, Name = "Unknown Player" };

        // Act
        var backups = _depthChartService.GetBackups(AppConstants.Teams.TampaBayBuccaneers, "QB", unknownPlayer);

        // Assert
        Assert.Empty(backups); // Unknown player is not in the depth chart
    }

}