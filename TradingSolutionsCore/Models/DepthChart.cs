namespace TradingSolutionsCore.Models;

public class DepthChart
{
    public string Position { get; set; }
    public List<Player> Players { get; private set; }

    public DepthChart(string position)
    {
        Position = position;
        Players = [];
    }

    public void AddPlayer(Player player, int? positionDepth = null)
    {
        if (positionDepth.HasValue)
        {
            if (positionDepth.Value < 0 || positionDepth.Value > Players.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(positionDepth), "Position depth is out of range.");
            }
            Players.Insert(positionDepth.Value, player);
        }
        else
        {
            Players.Add(player);
        }
    }

    public void RemovePlayer(Player player)
    {
        Players.Remove(player);
    }

    public List<Player> GetBackups(Player player)
    {
        int index = Players.FindIndex(p => p.Number == player.Number);
        if (index == -1 || index == Players.Count - 1)
        {
            return [];
        }
        return Players.Skip(index + 1).ToList();
    }
}