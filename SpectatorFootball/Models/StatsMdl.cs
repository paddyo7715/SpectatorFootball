namespace SpectatorFootball
{
    public class StatsMdl
    {
        public int Game_ID { get; set; } = 0;
        public int Player_ID { get; set; } = 0;

        public int Started { get; set; } = 0;
        public int Game_Played { get; set; } = 0;
        public int Fumbles { get; set; } = 0;

        // Passing
        public int Pass_Comp { get; set; } = 0;
        public int Pass_Att { get; set; } = 0;
        public int Pass_Yards { get; set; } = 0;
        public int Pass_TDs { get; set; } = 0;
        public int Pass_Ints { get; set; } = 0;
        public int Pass_Sacks { get; set; } = 0;

        // Offensive
        public int OLine_Sacks_Allowed { get; set; } = 0;
        public int OLine_Rushing_Loss_Allowed { get; set; } = 0;
        public int OLine_Pancakes { get; set; } = 0;
        public int Rush_Att { get; set; } = 0;
        public int Rush_Yards { get; set; } = 0;
        public int Rush_TDs { get; set; } = 0;
        public int Rec_Catches { get; set; } = 0;
        public int Rec_Drops { get; set; } = 0;
        public int Rec_Yards { get; set; } = 0;
        public int Rec_TDs { get; set; } = 0;

        // Defense
        public int Def_Sacks { get; set; } = 0;
        public int Def_Rushing_Loss { get; set; } = 0;
        public int Def_Tackles { get; set; } = 0;
        public int Def_Missed_Tackles { get; set; } = 0;
        public int Def_Ints { get; set; } = 0;
        public int Pass_Defenses { get; set; } = 0;
        public int Def_Interception_Yards { get; set; } = 0;
        public int Def_Safeties { get; set; } = 0;
        public int Def_TDs { get; set; } = 0;

        // Kicking Game
        public int num_punts { get; set; } = 0;
        public int Punt_yards { get; set; } = 0;
        public int Punt_killed_num { get; set; } = 0;
        public int FG_Att { get; set; } = 0;
        public int FG_Made { get; set; } = 0;
        public int XP_Att { get; set; } = 0;
        public int XP_Made { get; set; } = 0;

        public StatsMdl(int Game_ID, int Player_ID)
        {
            this.Game_ID = Game_ID;
            this.Player_ID = Player_ID;
        }
        public void setQBStats(int Started, int Game_Played, int Fumbles, int Pass_Comp, int Pass_Att, int Pass_Yards, int Pass_TDs, int Pass_Ints, int Pass_Sacks)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.Fumbles += Fumbles;
            this.Pass_Comp += Pass_Comp;
            this.Pass_Att += Pass_Att;
            this.Pass_Yards += Pass_Yards;
            this.Pass_TDs += Pass_TDs;
            this.Pass_Ints += Pass_Ints;
            this.Pass_Sacks += Pass_Sacks;
        }

        public void setRBStats(int Started, int Game_Played, int Fumbles, int Rush_Att, int Rush_Yards, int Rush_TDs, int Rec_Catches, int Rec_Drops, int Rec_Yards, int Rec_TDs)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.Fumbles += Fumbles;
            this.Rush_Att += Rush_Att;
            this.Rush_Yards += Rush_Yards;
            this.Rush_TDs += Rush_TDs;
            this.Rec_Catches += Rec_Catches;
            this.Rec_Drops += Rec_Drops;
            this.Rec_Yards += Rec_Yards;
            this.Rec_TDs += Rec_TDs;
        }

        public void setWRStats(int Started, int Game_Played, int Fumbles, int Rec_Catches, int Rec_Drops, int Rec_Yards, int Rec_TDs)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.Fumbles += Fumbles;
            this.Rec_Catches += Rec_Catches;
            this.Rec_Drops += Rec_Drops;
            this.Rec_Yards += Rec_Yards;
            this.Rec_TDs += Rec_TDs;
        }
        public void setTEStats(int Started, int Game_Played, int Fumbles, int Rec_Catches, int Rec_Drops, int Rec_Yards, int Rec_TDs, int OLine_Sacks_Allowed, int OLine_Rushing_Loss_Allowed, int OLine_Pancakes)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.Fumbles += Fumbles;
            this.Rec_Catches += Rec_Catches;
            this.Rec_Drops += Rec_Drops;
            this.Rec_Yards += Rec_Yards;
            this.Rec_TDs += Rec_TDs;
            this.OLine_Sacks_Allowed += OLine_Sacks_Allowed;
            this.OLine_Rushing_Loss_Allowed += OLine_Rushing_Loss_Allowed;
            this.OLine_Sacks_Allowed += OLine_Sacks_Allowed;
        }

        public void setOLStats(int Started, int Game_Played, int OLine_Sacks_Allowed, int OLine_Rushing_Loss_Allowed, int OLine_Pancakes)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.OLine_Sacks_Allowed += OLine_Sacks_Allowed;
            this.OLine_Rushing_Loss_Allowed += OLine_Rushing_Loss_Allowed;
            this.OLine_Sacks_Allowed += OLine_Sacks_Allowed;
        }

        public void setDLStats(int Started, int Game_Played, int Def_Sacks, int Def_Rushing_Loss, int Def_Tackles, int Def_Missed_Tackles, int Def_Safeties, int Def_TDs)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.Def_Sacks += Def_Sacks;
            this.Def_Rushing_Loss += Def_Rushing_Loss;
            this.Def_Tackles += Def_Tackles;
            this.Def_Missed_Tackles += Def_Missed_Tackles;
            this.Def_Safeties = Def_Safeties;
            this.Def_TDs = Def_TDs;
        }

        public void setLBStats(int Started, int Game_Played, int Def_Sacks, int Def_Rushing_Loss, int Def_Tackles, int Def_Missed_Tackles, int Def_Safeties, int Def_TDs, int Def_Ints, int Pass_Defenses, int Def_Interception_Yards)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.Def_Sacks += Def_Sacks;
            this.Def_Rushing_Loss += Def_Rushing_Loss;
            this.Def_Tackles += Def_Tackles;
            this.Def_Missed_Tackles += Def_Missed_Tackles;
            this.Def_Safeties = Def_Safeties;
            this.Def_TDs = Def_TDs;
            this.Def_Ints = Def_Ints;
            this.Pass_Defenses = Pass_Defenses;
            this.Def_Interception_Yards = Def_Interception_Yards;
        }

        public void setDBStats(int Started, int Game_Played, int Def_Tackles, int Def_Missed_Tackles, int Def_TDs, int Def_Ints, int Pass_Defenses, int Def_Interception_Yards)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.Def_Tackles += Def_Tackles;
            this.Def_Missed_Tackles += Def_Missed_Tackles;
            this.Def_TDs = Def_TDs;
            this.Def_Ints = Def_Ints;
            this.Pass_Defenses = Pass_Defenses;
            this.Def_Interception_Yards = Def_Interception_Yards;
        }

        public void setPStats(int Started, int Game_Played, int num_punts, int punt_yards, int punt_killed_num)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.num_punts = num_punts;
            Punt_yards = punt_yards;
            Punt_killed_num = punt_killed_num;
        }

        public void setKStats(int Started, int Game_Played, int FG_Att, int FG_Made, int XP_Att, int XP_Made)
        {
            this.Started += Started;
            this.Game_Played += Game_Played;
            this.FG_Att = FG_Att;
            this.FG_Made = FG_Made;
            this.XP_Att = XP_Att;
            this.XP_Made = XP_Made;
        }
    }
}
