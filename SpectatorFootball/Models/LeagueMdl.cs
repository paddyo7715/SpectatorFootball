using System.Collections.Generic;

namespace SpectatorFootball
{
    public class Leaguemdl
    {
        public enum League_State
        {
            Regular_Season,
            Playoffs,
            Season_Complete,
            Prev_Season
        }

        public string Short_Name { get; set; } = "";
        public string Long_Name { get; set; } = "";
        public int Starting_Year { get; set; }
        public int Number_of_weeks { get; set; }
        public int Number_of_Games { get; set; }
        public string Championship_Game_Name { get; set; } = "";
        public int Num_Teams { get; set; }
        public int Num_Playoff_Teams { get; set; }

        public List<int> Years { get; set; }

        public League_State State { get; set; } = default(League_State);

        public List<string> Conferences { get; set; } = new List<string>();
        public List<string> Divisions { get; set; } = new List<string>();

        public List<TeamMdl> Teams { get; set; } = new List<TeamMdl>();

        public List<string> Schedule { get; set; } = null;
        public void setOrganization(int Number_of_weeks, int Number_of_Games, int Num_Teams, int Num_Playoff_Teams)
        {
            this.Number_of_weeks = Number_of_weeks;
            this.Number_of_Games = Number_of_Games;
            this.Num_Teams = Num_Teams;
            this.Num_Playoff_Teams = Num_Playoff_Teams;

            Teams = new List<TeamMdl>();

            for (int i = 1; i <= this.Num_Teams; i++)
                Teams.Add(new TeamMdl(i, App_Constants.EMPTY_TEAM_SLOT));
        }
        public void setBasicInfo(string Short_Name, string Long_Name, int Starting_Year, string Championship_Game_Name, List<string> Conferences, List<string> Divisions, List<int> Years, League_State State)
        {
            this.Short_Name = Short_Name;
            this.Long_Name = Long_Name;
            this.Starting_Year = Starting_Year;
            this.Championship_Game_Name = Championship_Game_Name;

            this.Conferences = Conferences;
            this.Divisions = Divisions;

            this.Years = Years;
            this.State = State;
        }
        public void setSchedule(List<string> Schedule)
        {
            this.Schedule = Schedule;
        }
    }
}
