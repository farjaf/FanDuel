using Microsoft.AspNetCore.Mvc;
using TradingSolutions.Models;
using TradingSolutions.Services;

namespace TradingSolutionsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepthChartController : ControllerBase
    {
        private readonly IDepthChartService _service;

        public DepthChartController(IDepthChartService service)
        {
            _service = service;
        }

        [HttpPost("AddPlayer")]
        public IActionResult AddPlayer(string teamName, string position, Player player, int? positionDepth = null)
        {
            _service.AddPlayer(teamName, position, player, positionDepth);
            return Ok();
        }

        [HttpDelete("RemovePlayer")]
        public IActionResult RemovePlayer(string teamName, string position, Player player)
        {
            var removedPlayer = _service.RemovePlayer(teamName, position, player);

            return Ok(removedPlayer);
        }

        [HttpGet("GetBackups")]
        public IActionResult GetBackups(string teamName, string position, Player player)
        {
            var backups = _service.GetBackups(teamName, position, player);
            return Ok(backups);
        }

        [HttpGet("GetFullDepthChart")]
        public IActionResult GetFullDepthChart(string teamName)
        {
            var depthChart = _service.GetFullDepthChart(teamName);
            return Ok(depthChart);
        }
    }
}
