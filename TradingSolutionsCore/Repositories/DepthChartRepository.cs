using TradingSolutionsCore.Models;

namespace TradingSolutionsCore.Repositories
{
    public class DepthChartRepository : IDepthChartRepository
    {
        private readonly Sport _sport;

        public DepthChartRepository(Sport sport)
        {
            _sport = sport;
        }

        public void AddPlayer(string teamName, string position, Player player, int? positionDepth)
        {
            var team = _sport.GetTeam(teamName);
            var depthChart = team.GetDepthChart(position) ?? new DepthChart(position);
            depthChart.AddPlayer(player, positionDepth);
            team.AddDepthChart(position, depthChart);
        }

        public Player RemovePlayer(string teamName, string position, Player player)
        {
            var team = _sport.GetTeam(teamName);
            var depthChart = team.GetDepthChart(position);
            if (depthChart == null) return new Player();

            var playerToRemove = depthChart.Players.FirstOrDefault(p => p.Number == player.Number);

            if (playerToRemove == null) return new Player();

            depthChart.RemovePlayer(playerToRemove);
            return playerToRemove;

        }

        public List<Player> GetBackups(string teamName, string position, Player player)
        {
            var team = _sport.GetTeam(teamName);
            var depthChart = team.GetDepthChart(position);
            return depthChart?.GetBackups(player) ?? [];
        }

        public Dictionary<string, List<Player>> GetFullDepthChart(string teamName)
        {
            var team = _sport.GetTeam(teamName);
            var result = new Dictionary<string, List<Player>>();
            foreach (var depthChart in team.DepthCharts)
            {
                result[depthChart.Key] = depthChart.Value.Players;
            }
            return result;
        }
    }
}
