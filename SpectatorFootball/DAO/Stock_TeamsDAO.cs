using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SpectatorFootball
{
    public class Stock_TeamsDAO
    {
        public SQLiteConnection SettingsConnection { get; set; } = new SQLiteConnection();

        public Stock_TeamsDAO()
        {
            string constr = "";
            constr = (string)CommonUtils.getSettingsDBConnectionString();
            SettingsConnection.ConnectionString = constr;
        }

        public List<TeamMdl> getAllStockTeams()
        {
            List<TeamMdl> r = new List<TeamMdl>();

            SQLiteCommand cmd = null;

            int ID;
            string City_Abr;
            string City;
            string nickname;
            string Stadium_Name;
            string Stadium_Location;
            string Stadium_Capacity;
            string Stadium_Img_Path;
            string Helmet_img_path;
            string Helmet_Color;
            string Helmet_Logo_Color;
            string Helmet_Facemask_Color;
            string Socks_Color;
            string Cleats_Color;
            string Home_jersey_Color;
            string Home_Sleeve_Color;
            string Home_Jersey_Number_Color;
            string Home_Jersey_Number_Outline_Color;
            string Home_Jersey_Shoulder_Stripe;
            string Home_Jersey_Sleeve_Stripe_Color_1;
            string Home_Jersey_Sleeve_Stripe_Color_2;
            string Home_Jersey_Sleeve_Stripe_Color_3;
            string Home_Jersey_Sleeve_Stripe_Color_4;
            string Home_Jersey_Sleeve_Stripe_Color_5;
            string Home_Jersey_Sleeve_Stripe_Color_6;
            string Home_Pants_Color;
            string Home_Pants_Stripe_Color_1;
            string Home_Pants_Stripe_Color_2;
            string Home_Pants_Stripe_Color_3;
            string Away_jersey_Color;
            string Away_Sleeve_Color;
            string Away_Jersey_Number_Color;
            string Away_Jersey_Number_Outline_Color;
            string Away_Jersey_Shoulder_Stripe;
            string Away_Jersey_Sleeve_Stripe_Color_1;
            string Away_Jersey_Sleeve_Stripe_Color_2;
            string Away_Jersey_Sleeve_Stripe_Color_3;
            string Away_Jersey_Sleeve_Stripe_Color_4;
            string Away_Jersey_Sleeve_Stripe_Color_5;
            string Away_Jersey_Sleeve_Stripe_Color_6;
            string Away_Pants_Color;
            string Away_Pants_Stripe_Color_1;
            string Away_Pants_Stripe_Color_2;
            string Away_Pants_Stripe_Color_3;
            string Stadium_Field_Type;
            string Stadium_Field_Color;

            try
            {
                SettingsConnection.Open();
                string sSQL = @"SELECT 	ID, City_Abr, City,nickname,Stadium_Name, Stadium_Location,Stadium_Capacity,Stadium_Img_Path,
	        Helmet_img_path, Helmet_Color, Helmet_Logo_Color, Helmet_Facemask_Color, Socks_Color,
	        Cleats_Color, Home_jersey_Color, Home_Sleeve_Color, Home_Jersey_Number_Color, Home_Jersey_Number_Outline_Color,
	        Home_Jersey_Shoulder_Stripe,
	        Home_Jersey_Sleeve_Stripe_Color_1,
	        Home_Jersey_Sleeve_Stripe_Color_2,
	        Home_Jersey_Sleeve_Stripe_Color_3,
	        Home_Jersey_Sleeve_Stripe_Color_4,
	        Home_Jersey_Sleeve_Stripe_Color_5,
	        Home_Jersey_Sleeve_Stripe_Color_6,
	        Home_Pants_Color,
	        Home_Pants_Stripe_Color_1,
	        Home_Pants_Stripe_Color_2,
	        Home_Pants_Stripe_Color_3,
	        Away_jersey_Color,Away_Sleeve_Color,Away_Jersey_Number_Color, Away_Jersey_Number_Outline_Color,Away_Jersey_Shoulder_Stripe,
	        Away_Jersey_Sleeve_Stripe_Color_1,
	        Away_Jersey_Sleeve_Stripe_Color_2,
	        Away_Jersey_Sleeve_Stripe_Color_3,
	        Away_Jersey_Sleeve_Stripe_Color_4,
	        Away_Jersey_Sleeve_Stripe_Color_5,
	        Away_Jersey_Sleeve_Stripe_Color_6,
	        Away_Pants_Color,
	        Away_Pants_Stripe_Color_1,
	        Away_Pants_Stripe_Color_2,
	        Away_Pants_Stripe_Color_3,
	        Stadium_Field_Type, Stadium_Field_Color	
            FROM Stock_Teams order by City;";

                cmd = SettingsConnection.CreateCommand();
                cmd.CommandText = sSQL;

                SQLiteDataReader rdr = cmd.ExecuteReader();

                using (rdr)
                    while (rdr.Read())
                    {
                        ID = rdr.GetInt16(rdr.GetOrdinal("ID"));
                        City_Abr = rdr.GetString(rdr.GetOrdinal("City_Abr"));
                        City = rdr.GetString(rdr.GetOrdinal("City"));
                        nickname = rdr.GetString(rdr.GetOrdinal("nickname"));
                        Stadium_Name = rdr.GetString(rdr.GetOrdinal("Stadium_Name"));
                        Stadium_Location = rdr.GetString(rdr.GetOrdinal("Stadium_Location"));
                        Stadium_Capacity = rdr.GetString(rdr.GetOrdinal("Stadium_Capacity"));
                        Stadium_Img_Path = rdr.GetString(rdr.GetOrdinal("Stadium_Img_Path"));
                        Helmet_img_path = rdr.GetString(rdr.GetOrdinal("Helmet_img_path"));
                        Helmet_Color = rdr.GetString(rdr.GetOrdinal("Helmet_Color"));
                        Helmet_Logo_Color = rdr.GetString(rdr.GetOrdinal("Helmet_Logo_Color"));
                        Helmet_Facemask_Color = rdr.GetString(rdr.GetOrdinal("Helmet_Facemask_Color"));
                        Socks_Color = rdr.GetString(rdr.GetOrdinal("Socks_Color"));
                        Cleats_Color = rdr.GetString(rdr.GetOrdinal("Cleats_Color"));
                        Home_jersey_Color = rdr.GetString(rdr.GetOrdinal("Home_jersey_Color"));
                        Home_Sleeve_Color = rdr.GetString(rdr.GetOrdinal("Home_Sleeve_Color"));
                        Home_Jersey_Number_Color = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Number_Color"));
                        Home_Jersey_Number_Outline_Color = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Number_Outline_Color"));
                        Home_Jersey_Shoulder_Stripe = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Shoulder_Stripe"));
                        Home_Jersey_Sleeve_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_1"));
                        Home_Jersey_Sleeve_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_2"));
                        Home_Jersey_Sleeve_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_3"));
                        Home_Jersey_Sleeve_Stripe_Color_4 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_4"));
                        Home_Jersey_Sleeve_Stripe_Color_5 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_5"));
                        Home_Jersey_Sleeve_Stripe_Color_6 = rdr.GetString(rdr.GetOrdinal("Home_Jersey_Sleeve_Stripe_Color_6"));
                        Home_Pants_Color = rdr.GetString(rdr.GetOrdinal("Home_Pants_Color"));
                        Home_Pants_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Home_Pants_Stripe_Color_1"));
                        Home_Pants_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Home_Pants_Stripe_Color_2"));
                        Home_Pants_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Home_Pants_Stripe_Color_3"));
                        Away_jersey_Color = rdr.GetString(rdr.GetOrdinal("Away_jersey_Color"));
                        Away_Sleeve_Color = rdr.GetString(rdr.GetOrdinal("Away_Sleeve_Color"));
                        Away_Jersey_Number_Color = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Number_Color"));
                        Away_Jersey_Number_Outline_Color = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Number_Outline_Color"));
                        Away_Jersey_Shoulder_Stripe = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Shoulder_Stripe"));
                        Away_Jersey_Sleeve_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_1"));
                        Away_Jersey_Sleeve_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_2"));
                        Away_Jersey_Sleeve_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_3"));
                        Away_Jersey_Sleeve_Stripe_Color_4 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_4"));
                        Away_Jersey_Sleeve_Stripe_Color_5 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_5"));
                        Away_Jersey_Sleeve_Stripe_Color_6 = rdr.GetString(rdr.GetOrdinal("Away_Jersey_Sleeve_Stripe_Color_6"));
                        Away_Pants_Color = rdr.GetString(rdr.GetOrdinal("Away_Pants_Color"));
                        Away_Pants_Stripe_Color_1 = rdr.GetString(rdr.GetOrdinal("Away_Pants_Stripe_Color_1"));
                        Away_Pants_Stripe_Color_2 = rdr.GetString(rdr.GetOrdinal("Away_Pants_Stripe_Color_2"));
                        Away_Pants_Stripe_Color_3 = rdr.GetString(rdr.GetOrdinal("Away_Pants_Stripe_Color_3"));
                        Stadium_Field_Type = rdr.GetInt16(rdr.GetOrdinal("Stadium_Field_Type")).ToString();
                        Stadium_Field_Color = rdr.GetString(rdr.GetOrdinal("Stadium_Field_Color"));

                        StadiumMdl stadium = new StadiumMdl(Stadium_Name, Stadium_Location, Int32.Parse(Stadium_Field_Type), Stadium_Field_Color, Stadium_Capacity, Stadium_Img_Path);

                        FootwearMdl Footwear = new FootwearMdl(Socks_Color, Cleats_Color);

                        HelmetMdl Helmet = new HelmetMdl(Helmet_Color, Helmet_Logo_Color, Helmet_Facemask_Color);

                        JerseyMdl Home_Jersey = new JerseyMdl(Home_jersey_Color, Home_Sleeve_Color, Home_Jersey_Shoulder_Stripe, Home_Jersey_Number_Color, Home_Jersey_Number_Outline_Color, Home_Jersey_Sleeve_Stripe_Color_1, Home_Jersey_Sleeve_Stripe_Color_2, Home_Jersey_Sleeve_Stripe_Color_3, Home_Jersey_Sleeve_Stripe_Color_4, Home_Jersey_Sleeve_Stripe_Color_5, Home_Jersey_Sleeve_Stripe_Color_6);

                        PantsMdl Home_Pants = new PantsMdl(Home_Pants_Color, Home_Pants_Stripe_Color_1, Home_Pants_Stripe_Color_2, Home_Pants_Stripe_Color_3);

                        JerseyMdl Away_Jersey = new JerseyMdl(Away_jersey_Color, Away_Sleeve_Color, Away_Jersey_Shoulder_Stripe, Away_Jersey_Number_Color, Away_Jersey_Number_Outline_Color, Away_Jersey_Sleeve_Stripe_Color_1, Away_Jersey_Sleeve_Stripe_Color_2, Away_Jersey_Sleeve_Stripe_Color_3, Away_Jersey_Sleeve_Stripe_Color_4, Away_Jersey_Sleeve_Stripe_Color_5, Away_Jersey_Sleeve_Stripe_Color_6);

                        PantsMdl Away_Pants = new PantsMdl(Away_Pants_Color, Away_Pants_Stripe_Color_1, Away_Pants_Stripe_Color_2, Away_Pants_Stripe_Color_3);

                        UniformMdl Uniform = new UniformMdl(Helmet, Home_Jersey, Away_Jersey, Home_Pants, Away_Pants, Footwear);

                        TeamMdl Team = new TeamMdl(ID, City);
                        Team.setFields("", City_Abr, City, nickname, stadium, Uniform, Helmet_img_path);
                        r.Add(Team);
                    }
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }

            return r;
        }
        public void AddStockTeam(TeamMdl t)
        {
            string sSQL = null;

            SQLiteCommand cmdTeam = null;

            try
            {
                SettingsConnection.Open();
                sSQL = @"INSERT INTO Stock_Teams (City_Abr, City, Nickname, Helmet_img_path,
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
                        VALUES(@City_Abr, @City, @Nickname, @Helmet_img_path,
                        @Helmet_Color, @Helmet_Logo_Color,@Helmet_Facemask_Color, @Socks_Color, @Cleats_Color,  
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
                cmdTeam = SettingsConnection.CreateCommand();
                cmdTeam.CommandText = sSQL;
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
                int i = Convert.ToInt32(cmdTeam.ExecuteNonQuery());

                if (i != 1)
                    throw new Exception("Error inserting stock team " + Convert.ToString(i) + " rows inserted");
            }
            finally
            {
                if (cmdTeam != null)
                    cmdTeam.Dispose();
                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }
        }
        public void DeleteStockTeam(int t_id)
        {
            string sSQL = null;

            SQLiteCommand cmdTeam = null;

            try
            {
                SettingsConnection.Open();
                sSQL = "DELETE FROM STOCK_TEAMS WHERE ID = @ID";
                cmdTeam = SettingsConnection.CreateCommand();
                cmdTeam.CommandText = sSQL;
                cmdTeam.Parameters.Add("@ID", DbType.Int32).Value = t_id;
                int i = Convert.ToInt32(cmdTeam.ExecuteNonQuery());

                if (i != 1)
                    throw new Exception("Error deleting stock team ");
            }
            finally
            {
                if (cmdTeam != null)
                    cmdTeam.Dispose();
                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }
        }
        public void UpdateStockTeam(TeamMdl t)
        {
            string sSQL = null;

            SQLiteCommand cmdTeam = null;
            try
            {
                SettingsConnection.Open();
                sSQL = @"UPDATE Stock_Teams 
                    SET City_Abr =@City_Abr, 
                        City =@City,
                        Nickname =@Nickname,
                        Helmet_img_path =@Helmet_img_path,
                        Helmet_Color =@Helmet_Color,
                        Helmet_Logo_Color =@Helmet_Logo_Color,
                        Helmet_Facemask_Color =@Helmet_Facemask_Color,
                        Socks_Color =@Socks_Color,
                        Cleats_Color =@Cleats_Color,
                        Home_jersey_Color =@Home_jersey_Color,
                        Home_Sleeve_Color =@Home_Sleeve_Color,
                        Home_Jersey_Shoulder_Stripe =@Home_Jersey_Shoulder_Stripe,
                        Home_Jersey_Number_Color =@Home_Jersey_Number_Color,
                        Home_Jersey_Number_Outline_Color =@Home_Jersey_Number_Outline_Color,
                        Home_Jersey_Sleeve_Stripe_Color_1 =@Home_Jersey_Sleeve_Stripe_Color_1,
                        Home_Jersey_Sleeve_Stripe_Color_2 =@Home_Jersey_Sleeve_Stripe_Color_2,
                        Home_Jersey_Sleeve_Stripe_Color_3 =@Home_Jersey_Sleeve_Stripe_Color_3,
                        Home_Jersey_Sleeve_Stripe_Color_4 =@Home_Jersey_Sleeve_Stripe_Color_4,
                        Home_Jersey_Sleeve_Stripe_Color_5 =@Home_Jersey_Sleeve_Stripe_Color_5,
                        Home_Jersey_Sleeve_Stripe_Color_6 =@Home_Jersey_Sleeve_Stripe_Color_6,                       
                        Home_Pants_Color =@Home_Pants_Color,
                        Home_Pants_Stripe_Color_1 =@Home_Pants_Stripe_Color_1,
                        Home_Pants_Stripe_Color_2 =@Home_Pants_Stripe_Color_2,
                        Home_Pants_Stripe_Color_3 =@Home_Pants_Stripe_Color_3,
                        Away_jersey_Color = @Away_jersey_Color,
                        Away_Sleeve_Color = @Away_Sleeve_Color,
                        Away_Jersey_Shoulder_Stripe =@Away_Jersey_Shoulder_Stripe,
                        Away_Jersey_Number_Color =@Away_Jersey_Number_Color,
                        Away_Jersey_Number_Outline_Color =@Away_Jersey_Number_Outline_Color,
                        Away_Jersey_Sleeve_Stripe_Color_1 =@Away_Jersey_Sleeve_Stripe_Color_1,
                        Away_Jersey_Sleeve_Stripe_Color_2 =@Away_Jersey_Sleeve_Stripe_Color_2,
                        Away_Jersey_Sleeve_Stripe_Color_3 =@Away_Jersey_Sleeve_Stripe_Color_3,
                        Away_Jersey_Sleeve_Stripe_Color_4 =@Away_Jersey_Sleeve_Stripe_Color_4,
                        Away_Jersey_Sleeve_Stripe_Color_5 =@Away_Jersey_Sleeve_Stripe_Color_5,
                        Away_Jersey_Sleeve_Stripe_Color_6 =@Away_Jersey_Sleeve_Stripe_Color_6,   
                        Away_Pants_Color =@Away_Pants_Color,
                        Away_Pants_Stripe_Color_1 =@Away_Pants_Stripe_Color_1,
                        Away_Pants_Stripe_Color_2 =@Away_Pants_Stripe_Color_2,
                        Away_Pants_Stripe_Color_3 =@Away_Pants_Stripe_Color_3,
                        Stadium_Name =@Stadium_Name,
                        Stadium_Location = @Stadium_Location,
                        Stadium_Field_Type =@Stadium_Field_Type,
                        Stadium_Field_Color =@Stadium_Field_Color,
                        Stadium_Capacity =@Stadium_Capacity,
                        Stadium_Img_Path = @Stadium_Img_path
                        WHERE ID = @ID";
                cmdTeam = SettingsConnection.CreateCommand();
                cmdTeam.CommandText = sSQL;
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
                cmdTeam.Parameters.Add("@ID", DbType.Int16).Value = t.id;
                int i = Convert.ToInt32(cmdTeam.ExecuteNonQuery());

                if (i != 1)
                    throw new Exception("Error editing stock team " + Convert.ToString(i) + " rows edited");
            }
            finally
            {
                if (cmdTeam != null)
                    cmdTeam.Dispose();
                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }
        }
        public bool DoesTeamAlreadyExist(string City, string Nickname)
        {
            bool r = false;

            string sSQL = null;

            SQLiteCommand cmdTeam = null;
            try
            {
                SettingsConnection.Open();

                sSQL = "select count(*) from Stock_Teams where upper(City) = @City and upper(Nickname) = @Nickname";
                cmdTeam = SettingsConnection.CreateCommand();
                cmdTeam.CommandText = sSQL;
                cmdTeam.Parameters.Add("@City", DbType.String).Value = City.ToUpper().Trim();
                cmdTeam.Parameters.Add("@Nickname", DbType.String).Value = Nickname.ToUpper().Trim();

                int i = Convert.ToInt32(cmdTeam.ExecuteScalar());

                if (i > 0)
                    r = true;

                return r;
            }
            finally
            {
                if (cmdTeam != null)
                    cmdTeam.Dispose();
                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }
        }

        public bool DoesTeamAlreadyExist_ID(string City, string Nickname, string original_city, string original_nickname)
        {
            bool r = false;

            string sSQL = null;

            SQLiteCommand cmdTeam = null;
            try
            {
                SettingsConnection.Open();

                sSQL = "select count(*) from Stock_Teams where upper(City) = @City and upper(Nickname) = @Nickname and ID <> (select ID from Stock_Teams where upper(City) = @original_City and upper(Nickname) = @original_Nickname);";
                cmdTeam = SettingsConnection.CreateCommand();
                cmdTeam.CommandText = sSQL;
                cmdTeam.Parameters.Add("@City", DbType.String).Value = City.ToUpper().Trim();
                cmdTeam.Parameters.Add("@Nickname", DbType.String).Value = Nickname.ToUpper().Trim();
                cmdTeam.Parameters.Add("@original_City", DbType.String).Value = original_city.ToUpper().Trim();
                cmdTeam.Parameters.Add("@original_Nickname", DbType.String).Value = original_nickname.ToUpper().Trim();

                int i = Convert.ToInt32(cmdTeam.ExecuteScalar());

                if (i > 0)
                    r = true;

                return r;
            }
            finally
            {
                if (cmdTeam != null)
                    cmdTeam.Dispose();
                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }
        }
    }
}
