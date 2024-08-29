using TradingSolutions.Models;

namespace TradingSolutions.Services;

public interface IDepthChartService
{
    void AddPlayerToDepthChart(string position, Player player, int? positionDepth = null);

    Player RemovePlayerFromDepthChart(string position, Player player);

    List<Player> GetBackups(string position, Player player);

    Dictionary<string, List<Player>> GetFullDepthChart();
}