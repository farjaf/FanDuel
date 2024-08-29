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

        if (positionDepth.HasValue)
        {
            int depth = positionDepth.Value;

            if (depth < 0 || depth > _depthChart[position].Count)
            {
                throw new ArgumentOutOfRangeException(nameof(positionDepth), "Position depth is out of range.");
            }

            // Insert the player at the specified depth, shifting others down
            _depthChart[position].Insert(depth, player);
        }
        else
        {
            // Add the player to the end if no depth is specified
            _depthChart[position].Add(player);
        }
    }

    public Player RemovePlayerFromDepthChart(string position, Player player)
    {
        if (_depthChart.TryGetValue(position, out var players))
        {
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
        if (_depthChart.TryGetValue(position, out var players))
        {
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