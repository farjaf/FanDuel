using TradingSolutionsCore.Models;
using TradingSolutionsCore.Repositories;

namespace TradingSolutionsCore.Services;


public class DepthChartService : IDepthChartService
{
    private readonly IDepthChartRepository _repository;

    public DepthChartService(IDepthChartRepository repository)
    {
        _repository = repository;
    }

    public void AddPlayer(string teamName, string position, Player player, int? positionDepth)
    {
        _repository.AddPlayer(teamName, position, player, positionDepth);
    }

    public Player RemovePlayer(string teamName, string position, Player player)
    {
        return _repository.RemovePlayer(teamName, position, player);
    }

    public List<Player> GetBackups(string teamName, string position, Player player)
    {
        return _repository.GetBackups(teamName, position, player);
    }

    public Dictionary<string, List<Player>> GetFullDepthChart(string teamName)
    {
        return _repository.GetFullDepthChart(teamName);
    }
}