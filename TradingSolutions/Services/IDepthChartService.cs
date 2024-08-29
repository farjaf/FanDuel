using TradingSolutions.Models;

namespace TradingSolutions.Services;

public interface IDepthChartService
{
    void AddPlayer(string teamName, string position, Player player, int? positionDepth);
    Player RemovePlayer(string teamName, string position, Player player);
    List<Player> GetBackups(string teamName, string position, Player player);
    Dictionary<string, List<Player>> GetFullDepthChart(string teamName);
}