using TradingSolutions.Models;

namespace TradingSolutions.Services;

public class DepthChartService : IDepthChartService
{
    private readonly Dictionary<string, List<Player>> _depthChart = new();

    public void AddPlayerToDepthChart(string position, Player player, int? positionDepth = null)
    {
        if (!_depthChart.ContainsKey(position))
        {
            _depthChart[position] = [];
        }

        if (positionDepth is 0)
        {
            _depthChart[position].Insert(0, player);
        }
        else
        {
            _depthChart[position].Add(player);
        }
    }

    public Player RemovePlayerFromDepthChart(string position, Player player)
    {
        if (_depthChart.ContainsKey(position))
        {
            var players = _depthChart[position];
            var playerToRemove = players.FirstOrDefault(p => p.Number == player.Number);
            if (playerToRemove != null)
            {
                players.Remove(playerToRemove);
                return playerToRemove;
            }
        }

        return new Player();
    }

    public List<Player> GetBackups(string position, Player player)
    {
        if (_depthChart.ContainsKey(position))
        {
            var players = _depthChart[position];
            var playerIndex = players.FindIndex(p => p.Number == player.Number);
            if (playerIndex >= 0 && playerIndex < players.Count - 1)
            {
                return players.Skip(playerIndex + 1).ToList();
            }
        }
        return [];
    }

    public Dictionary<string, List<Player>> GetFullDepthChart()
    {
        return _depthChart;
    }
}