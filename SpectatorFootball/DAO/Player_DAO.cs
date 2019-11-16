using System;
using System.Data;
using System.Data.SQLite;

namespace SpectatorFootball
{
    public class Player_DAO
    {
        public static bool isPlayerNumber_Unigue_DB(string number, string league_db_connection, int team_ind)
        {
            SQLiteConnection LeagueConnection = new SQLiteConnection();
            LeagueConnection.ConnectionString = league_db_connection;
            SQLiteCommand cmd = null;
            bool r = false;

            try
            {
                LeagueConnection.Open();
                string sSQL = "select count(*) from Players where Jersey_Number = @JerseyNumber and Active = 1";
                cmd = LeagueConnection.CreateCommand();
                cmd.CommandText = sSQL;
                cmd.Parameters.Add("@JerseyNumber", DbType.Int16).Value = int.Parse(number.Trim());

                int i = cmd.ExecuteNonQuery();

                if (i == 0)
                    r = true;

                return r;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if ((int)LeagueConnection.State == (int)ConnectionState.Open)
                    LeagueConnection.Close();
            }
        }
    }
}
