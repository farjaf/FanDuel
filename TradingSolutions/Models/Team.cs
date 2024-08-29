namespace TradingSolutions.Models
{
    public class Team
    {
        public string Name { get; private set; }
        public Dictionary<string, DepthChart> DepthCharts { get; private set; }

        public Team(string name)
        {
            Name = name;
            DepthCharts = new Dictionary<string, DepthChart>();
        }

        public void AddDepthChart(string position, DepthChart depthChart)
        {
            DepthCharts[position] = depthChart;
        }

        public DepthChart GetDepthChart(string position)
        {
            return DepthCharts.ContainsKey(position) ? DepthCharts[position] : null;
        }
    }
}
