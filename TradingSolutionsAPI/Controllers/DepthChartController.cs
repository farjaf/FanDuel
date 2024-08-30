using Microsoft.AspNetCore.Mvc;
using TradingSolutionsCore.Common;
using TradingSolutionsCore.Models;
using TradingSolutionsCore.Services;

namespace TradingSolutionsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepthChartController(IDepthChartService service) : ControllerBase
    {
        [HttpPost("AddPlayerToDepthChart")]
        public IActionResult AddPlayer(string position, Player player, int? positionDepth = null)
        {
            service.AddPlayer(AppConstants.Teams.TampaBayBuccaneers, position, player, positionDepth);
            
            return Ok();
        }

        [HttpDelete("RemovePlayerFromDepthChart")]
        public IActionResult RemovePlayer(string position, Player player)
        {
            var removedPlayer = service.RemovePlayer(AppConstants.Teams.TampaBayBuccaneers, position, player);

            return Ok(removedPlayer);
        }

        [HttpPost("GetBackups")]
        public IActionResult GetBackups(string position, Player player)
        {
            var backups = service.GetBackups(AppConstants.Teams.TampaBayBuccaneers, position, player);
            
            return Ok(backups);
        }

        [HttpGet("GetFullDepthChart")]
        public IActionResult GetFullDepthChart()
        {
            var depthChart = service.GetFullDepthChart(AppConstants.Teams.TampaBayBuccaneers);
            
            return Ok(depthChart);
        }
    }
}
