using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using log4net;

namespace SpectatorFootball
{
    public class LeagueDAO
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        public SQLiteConnection League_con { get; set; } = null;
        public Leaguemdl nl { get; set; }

        public LeagueDAO(Leaguemdl nl)
        {
            this.nl = nl;
        }

        public SQLiteConnection getLeagueConnection(string sLeagueFileLocation)
        {
            SQLiteConnection c = new SQLiteConnection();
            c.ConnectionString = "Data Source=" + sLeagueFileLocation + ";";
            return c;
        }
        public SQLiteTransaction createTransaction(SQLiteConnection c)
        {
            return c.BeginTransaction();
        }
        public void closeConnection(SQLiteConnection c)
        {
            if ((int)c.State == (int)ConnectionState.Open)
                c.Close();
        }
        public void Create_New_League(string con_string)
        {
            SQLiteTransaction tr = null;
            SQLiteCommand cmdLeague = null;
            SQLiteCommand cmdConf = null;
            SQLiteCommand cmdDiv = null;
            SQLiteCommand cmdTeam = null;
            SQLiteCommand cmdPlayers = null;
            SQLiteCommand cmdGames = null;
            string strStage = null;
            string sSQL = null;

            try
            {
                strStage = "Getting connection";
                logger.Debug(strStage);
                League_con = getLeagueConnection(con_string);
                League_con.Open();

                strStage = "Beginning transaction";
                logger.Debug(strStage);
                tr = League_con.BeginTransaction();

                strStage = "Inserting League Record";
                logger.Debug(strStage);
                sSQL = "INSERT INTO LEAGUE (Short_Name, Long_Name,Starting_Year, Number_of_weeks,Number_of_Games, Champtionship_Game_Name, Num_Teams, Playoff_Teams) VALUES(@Short_Name, @Long_Name, @Starting_Year, @Number_of_weeks,@Number_of_Games, @Champtionship_Game_Name, @Num_Teams, @Playoff_Teams)";

                cmdLeague = new SQLiteCommand(League_con);
                cmdLeague.CommandText = sSQL;
                cmdLeague.Parameters.Add("@Short_Name", DbType.String).Value = nl.Short_Name;
                cmdLeague.Parameters.Add("@Long_Name", DbType.String).Value = nl.Long_Name;
                cmdLeague.Parameters.Add("@Starting_Year", DbType.Int16).Value = nl.Starting_Year;
                cmdLeague.Parameters.Add("@Number_of_weeks", DbType.Int16).Value = nl.Number_of_weeks;
                cmdLeague.Parameters.Add("@Number_of_Games", DbType.Int16).Value = nl.Number_of_Games;
                cmdLeague.Parameters.Add("@Champtionship_Game_Name", DbType.String).Value = nl.Championship_Game_Name;
                cmdLeague.Parameters.Add("@Num_Teams", DbType.Int16).Value = nl.Num_Teams;
                cmdLeague.Parameters.Add("@Playoff_Teams", DbType.Int16).Value = nl.Num_Playoff_Teams;
                cmdLeague.ExecuteNonQuery();

                strStage = "Inserting Conferences";
                logger.Debug(strStage);
                cmdConf = new SQLiteCommand(League_con);
                int c_id = 0;
                foreach (String c in nl.Conferences)
                {
                    c_id += 1;
                    logger.Debug("Inserting conference " + Convert.ToString(c_id));
                    sSQL = "INSERT INTO CONFERENCE (ID,Name) VALUES(@ID, @Conf_Name)";
                    cmdConf.CommandText = sSQL;
                    cmdConf.Parameters.Add("@ID", DbType.Int16).Value = c_id + 1;
                    cmdConf.Parameters.Add("@Conf_Name", DbType.String).Value = c;
                    cmdConf.ExecuteNonQuery();
                }

                strStage = "Inserting Divisions";
                logger.Debug(strStage);
                cmdDiv = new SQLiteCommand(League_con);

                for (int i = 1; i <= (nl.Num_Teams / nl.Divisions.Count); i++)
                {
                    string d = nl.Divisions[i];
                    logger.Debug("Inserting division " + d);
                    sSQL = "INSERT INTO DIVISION (ID, Name) VALUES(@ID, @Name)";
                    cmdDiv.CommandText = sSQL;
                    cmdDiv.Parameters.Add("@ID", DbType.Int16).Value = i;
                    cmdDiv.Parameters.Add("@Name", DbType.String).Value = d;
                    cmdDiv.ExecuteNonQuery();
                }

                strStage = "Inserting Team";
                logger.Debug(strStage);
                int t_id = 0;
                foreach (TeamMdl t in nl.Teams)
                {
                    logger.Debug("Inserting team " + t.Nickname);
                    t_id += 1;
                    int d_num = CommonUtils.getDivisionNum_from_Team_Number(nl.Num_Teams / nl.Divisions.Count, t_id);
                    int c_num = CommonUtils.getConferenceNum_from_Team_Number(nl.Conferences.Count, t_id);
                    sSQL = @"INSERT INTO TEAMS (ID, Owner, Division_ID, Conf_ID, City_Abr, City, Nickname, Helmet_img_path,
                        Helmet_Color, Helmet_Logo_Color, Helmet_Facemask_Color, Socks_Color, Cleats_Color,
                        Home_jersey_Color,Home_Sleeve_Color, Home_Jersey_Shoulder_Stripe, Home_Jersey_Number_Color, Home_Jersey_Number_Outline_Color,
                        Home_Jersey_Sleeve_Stripe_Color_1, Home_Jersey_Sleeve_Stripe_Color_2,
                        Home_Jersey_Sleeve_Stripe_Color_3, Home_Jersey_Sleeve_Stripe_Color_4,
                        Home_Jersey_Sleeve_Stripe_Color_5, Home_Jersey_Sleeve_Stripe_Color_6,                       
                        Home_Pants_Color, Home_Pants_Stripe_Color_1,  Home_Pants_Stripe_Color_2, Home_Pants_Stripe_Color_3,
                        Away_jersey_Color, Away_Sleeve_Color,Away_Jersey_Shoulder_Stripe, Away_Jersey_Number_Color, Away_Jersey_Number_Outline_Color,
                        Away_Jersey_Sleeve_Stripe_Color_1, Away_Jersey_Sleeve_Stripe_Color_2,
                        Away_Jersey_Sleeve_Stripe_Color_3, Away_Jersey_Sleeve_Stripe_Color_4,
                        Away_Jersey_Sleeve_Stripe_Color_5, Away_Jersey_Sleeve_Stripe_Color_6,   
                        Away_Pants_Color, Away_Pants_Stripe_Color_1,  Away_Pants_Stripe_Color_2, Away_Pants_Stripe_Color_3,
                        Stadium_Name,Stadium_Location,Stadium_Field_Type,Stadium_Field_Color,Stadium_Capacity,Stadium_Img_Path) 
                        VALUES(@ID, @Owner, @Division_ID, @Conf_ID, @City_Abr, @City, @Nickname, @Helmet_img_path,
                        @Helmet_Color, @Helmet_Logo_Color,@Helmet_Facemask_Color,   
                        @Home_jersey_Color,@Home_Sleeve_Color, @Home_Jersey_Shoulder_Stripe, @Home_Jersey_Number_Color, @Home_Jersey_Number_Outline_Color,
                        @Home_Jersey_Sleeve_Stripe_Color_1, @Home_Jersey_Sleeve_Stripe_Color_2,
                        @Home_Jersey_Sleeve_Stripe_Color_3, @Home_Jersey_Sleeve_Stripe_Color_4,
                        @Home_Jersey_Sleeve_Stripe_Color_5, @Home_Jersey_Sleeve_Stripe_Color_6,
                        @Home_Pants_Color, @Home_Pants_Stripe_Color_1, @Home_Pants_Stripe_Color_2, @Home_Pants_Stripe_Color_3,
                        @Away_jersey_Color, @Away_Sleeve_Color, @Away_Jersey_Shoulder_Stripe, @Away_Jersey_Number_Color, @Away_Jersey_Number_Outline_Color,
                        @Away_Jersey_Sleeve_Stripe_Color_1, @Away_Jersey_Sleeve_Stripe_Color_2,
                        @Away_Jersey_Sleeve_Stripe_Color_3, @Away_Jersey_Sleeve_Stripe_Color_4,
                        @Away_Jersey_Sleeve_Stripe_Color_5, @Away_Jersey_Sleeve_Stripe_Color_6,                       
                        @Away_Pants_Color, @Away_Pants_Stripe_Color_1, @Away_Pants_Stripe_Color_2, @Away_Pants_Stripe_Color_3,
                        @Stadium_Name,@Stadium_Location,@Stadium_Field_Type,@Stadium_Field_Color,@Stadium_Capacity,@Stadium_Img_path)";
                    cmdTeam.CommandText = sSQL;
                    cmdTeam.Parameters.Add("@ID", DbType.Int16).Value = t.id;
                    cmdTeam.Parameters.Add("@Owner", DbType.String).Value = t.Owner;
                    cmdTeam.Parameters.Add("@Division_ID", DbType.Int16).Value = d_num;
                    cmdTeam.Parameters.Add("@Conf_ID", DbType.Int16).Value = c_num;
                    cmdTeam.Parameters.Add("@City_Abr", DbType.String).Value = t.City_Abr;
                    cmdTeam.Parameters.Add("@City", DbType.String).Value = t.City;
                    cmdTeam.Parameters.Add("@Nickname", DbType.String).Value = t.Nickname;
                    cmdTeam.Parameters.Add("@Helmet_img_path", DbType.String).Value = Path.GetFileName(t.Helmet_img_path);
                    cmdTeam.Parameters.Add("@Helmet_Color", DbType.String).Value = t.Uniform.Helmet.Helmet_Color;
                    cmdTeam.Parameters.Add("@Helmet_Logo_Color", DbType.String).Value = t.Uniform.Helmet.Helmet_Logo_Color;
                    cmdTeam.Parameters.Add("@Helmet_Facemask_Color", DbType.String).Value = t.Uniform.Helmet.Helmet_Facemask_Color;
                    cmdTeam.Parameters.Add("@Socks_Color", DbType.String).Value = t.Uniform.Footwear.Socks_Color;
                    cmdTeam.Parameters.Add("@Cleats_Color", DbType.String).Value = t.Uniform.Footwear.Cleats_Color;
                    cmdTeam.Parameters.Add("@Home_jersey_Color", DbType.String).Value = t.Uniform.Home_Jersey.Jersey_Color;
                    cmdTeam.Parameters.Add("@Home_Sleeve_Color", DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Color;
                    cmdTeam.Parameters.Add("@Home_jersey_Shoulder_Stripe", DbType.String).Value = t.Uniform.Home_Jersey.Shoulder_Stripe_Color;
                    cmdTeam.Parameters.Add("@Home_Jersey_Number_Color", DbType.String).Value = t.Uniform.Home_Jersey.Number_Color;
                    cmdTeam.Parameters.Add("@Home_Jersey_Number_Outline_Color", DbType.String).Value = t.Uniform.Home_Jersey.Number_Outline_Color;
                    cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_1", DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe1;
                    cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_2", DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe2;
                    cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_3", DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe3;
                    cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_4", DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe4;
                    cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_5", DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe5;
                    cmdTeam.Parameters.Add("@Home_Jersey_Sleeve_Stripe_Color_6", DbType.String).Value = t.Uniform.Home_Jersey.Sleeve_Stripe6;
                    cmdTeam.Parameters.Add("@Home_Pants_Color", DbType.String).Value = t.Uniform.Home_Pants.Pants_Color;
                    cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_1", DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_1;
                    cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_2", DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_2;
                    cmdTeam.Parameters.Add("@Home_Pants_Stripe_Color_3", DbType.String).Value = t.Uniform.Home_Pants.Stripe_Color_3;
                    cmdTeam.Parameters.Add("@Away_jersey_Color", DbType.String).Value = t.Uniform.Away_Jersey.Jersey_Color;
                    cmdTeam.Parameters.Add("@Away_Sleeve_Color", DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Color;
                    cmdTeam.Parameters.Add("@Away_jersey_Shoulder_Stripe", DbType.String).Value = t.Uniform.Away_Jersey.Shoulder_Stripe_Color;
                    cmdTeam.Parameters.Add("@Away_Jersey_Number_Color", DbType.String).Value = t.Uniform.Away_Jersey.Number_Color;
                    cmdTeam.Parameters.Add("@Away_Jersey_Number_Outline_Color", DbType.String).Value = t.Uniform.Away_Jersey.Number_Outline_Color;
                    cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_1", DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe1;
                    cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_2", DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe2;
                    cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_3", DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe3;
                    cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_4", DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe4;
                    cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_5", DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe5;
                    cmdTeam.Parameters.Add("@Away_Jersey_Sleeve_Stripe_Color_6", DbType.String).Value = t.Uniform.Away_Jersey.Sleeve_Stripe6;
                    cmdTeam.Parameters.Add("@Away_Pants_Color", DbType.String).Value = t.Uniform.Away_Pants.Pants_Color;
                    cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_1", DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_1;
                    cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_2", DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_2;
                    cmdTeam.Parameters.Add("@Away_Pants_Stripe_Color_3", DbType.String).Value = t.Uniform.Away_Pants.Stripe_Color_3;
                    cmdTeam.Parameters.Add("@Stadium_Name", DbType.String).Value = t.Stadium.Stadium_Name;
                    cmdTeam.Parameters.Add("@Stadium_Location", DbType.String).Value = t.Stadium.Stadium_Location;
                    cmdTeam.Parameters.Add("@Stadium_Field_Type", DbType.Int16).Value = t.Stadium.Field_Type;
                    cmdTeam.Parameters.Add("@Stadium_Field_Color", DbType.String).Value = t.Stadium.Field_Color;
                    cmdTeam.Parameters.Add("@Stadium_Capacity", DbType.String).Value = t.Stadium.Capacity;
                    cmdTeam.Parameters.Add("@Stadium_Img_path", DbType.String).Value = Path.GetFileName(t.Stadium.Stadium_Img_Path);
                    cmdTeam.ExecuteNonQuery();

                    strStage = "Inserting Players";
                    logger.Debug(strStage);
                    foreach (PlayerMdl P in t.Players)
                    {
                        sSQL = @"INSERT INTO PLAYERS (Team_ID,Age,Jersey_Number,First_Name,Last_Name,Active,Position,Fumble_Rating,Accuracy_Rating,Decision_Making,Arm_Strength_Rating,Pass_Block_Rating,Run_Block_Rating,Running_Power_Rating,Speed_Rating,Agility_Rating,Hands_Rating,Pass_Attack,Run_Attack,Tackle_Rating,Kicker_Leg_Power,Kicker_Leg_Accuracy) 
                        VALUES(@Team_ID,@Age,@Jersey_Number,@First_Name,@Last_Name,@Active,@Position,@Fumble_Rating,
@Accuracy_Rating,@Decision_Making,@Arm_Strength_Rating,@Pass_Block_Rating,@Run_Block_Rating,@Running_Power_Rating,@Speed_Rating,
@Agility_Rating,@Hands_Rating,@Pass_Attack,@Run_Attack,@Tackle_Rating)";
                        cmdPlayers.CommandText = sSQL;
                        cmdPlayers.Parameters.Add("@Team_ID", DbType.Int16).Value = t_id;
                        cmdPlayers.Parameters.Add("@Age", DbType.Int16).Value = P.Age;
                        cmdPlayers.Parameters.Add("@Jersey_Number", DbType.Int16).Value = P.Jersey_Number;
                        cmdPlayers.Parameters.Add("@First_Name", DbType.String).Value = P.First_Name;
                        cmdPlayers.Parameters.Add("@Last_Name", DbType.String).Value = P.Last_Name;
                        cmdPlayers.Parameters.Add("@Active", DbType.Int16).Value = 1;
                        cmdPlayers.Parameters.Add("@Position", DbType.Int16).Value = P.Pos;
                        cmdPlayers.Parameters.Add("@Fumble_Rating", DbType.Int16).Value = P.Ratings.Fumble_Rating;
                        cmdPlayers.Parameters.Add("@Accuracy_Rating", DbType.Int16).Value = P.Ratings.Accuracy_Rating;
                        cmdPlayers.Parameters.Add("@Decision_Making", DbType.Int16).Value = P.Ratings.Decision_Making;
                        cmdPlayers.Parameters.Add("@Arm_Strength", DbType.Int16).Value = P.Ratings.Arm_Strength_Rating;
                        cmdPlayers.Parameters.Add("@Pass_Block_Rating", DbType.Int16).Value = P.Ratings.Pass_Block_Rating;
                        cmdPlayers.Parameters.Add("@Run_Block_Rating", DbType.Int16).Value = P.Ratings.Run_Block_Rating;
                        cmdPlayers.Parameters.Add("@Running_Power_Rating", DbType.Int16).Value = P.Ratings.Running_Power_Rating;
                        cmdPlayers.Parameters.Add("@Speed_Rating", DbType.Int16).Value = P.Ratings.Speed_Rating;
                        cmdPlayers.Parameters.Add("@Agility_Rating", DbType.Int16).Value = P.Ratings.Agilty_Rating;
                        cmdPlayers.Parameters.Add("@Hands_Rating", DbType.Int16).Value = P.Ratings.Hands_Rating;
                        cmdPlayers.Parameters.Add("@Pass_Attack", DbType.Int16).Value = P.Ratings.Pass_Attack;
                        cmdPlayers.Parameters.Add("@Run_Attack", DbType.Int16).Value = P.Ratings.Run_Attack;
                        cmdPlayers.Parameters.Add("@Tackle_Rating", DbType.Int16).Value = P.Ratings.Tackle_Rating;
                        cmdPlayers.Parameters.Add("@Kicker_Leg_Power", DbType.Int16).Value = P.Ratings.Leg_Strength;
                        cmdPlayers.Parameters.Add("@Kicker_Leg_Accuracy", DbType.Int16).Value = P.Ratings.Kicking_Accuracy;
                        cmdPlayers.ExecuteNonQuery();
                    }
                }


                strStage = "Inserting schedule into database";
                logger.Debug(strStage);
                string w = null;
                string h = null;
                string a = null;

                List<string> s = nl.Schedule;

                foreach (string g in s)
                {
                    string[] m = g.Split(',');

                    w = m[0];
                    h = m[1];
                    a = m[2];

                    sSQL = @"INSERT INTO Game (Year,Week,Home_Team_ID,Away_Team_ID 
                        VALUES(@Year,@Week,@Home_Team_ID,@Away_Team_ID)";
                    cmdGames.CommandText = sSQL;
                    cmdGames.Parameters.Add("@Year", DbType.Int16).Value = nl.Starting_Year;
                    cmdGames.Parameters.Add("@Week", DbType.Int16).Value = w;
                    cmdGames.Parameters.Add("@Home_Team_ID", DbType.Int16).Value = h;
                    cmdGames.Parameters.Add("@Away_Team_ID", DbType.String).Value = a;
                    cmdGames.ExecuteNonQuery();
                }

                tr.Commit();
                logger.Info("League successfuly saved to database.");
            }
            catch (Exception ex)
            {
                tr.Rollback();
                logger.Error("Error inserting league into db");
                logger.Error(ex);
                throw new Exception("Error at stage " + strStage + " writing records to database:" + ex.Message);
            }
            finally
            {
                closeConnection(League_con);
            }
        }
    }
}
