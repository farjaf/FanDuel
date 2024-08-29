namespace TradingSolutions.Models
{
    public class Sport
    {
        public string Name { get; private set; }
        public Dictionary<string, Team> Teams { get; private set; }

        public Sport(string name)
        {
            Name = name;
            Teams = new Dictionary<string, Team>();
        }

        public void AddTeam(string teamName)
        {
            if (!Teams.ContainsKey(teamName))
            {
                Teams[teamName] = new Team(teamName);
            }
        }

        public Team GetTeam(string teamName)
        {
            if (Teams.TryGetValue(teamName, out var team))
            {
                return team;
            }

            throw new KeyNotFoundException("Team not found.");
        }
    }
}
