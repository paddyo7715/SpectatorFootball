using System;
using System.Data.SQLite;
using System.Data;

namespace SpectatorFootball
{
    public class Player_NamesDAO
    {
        public SQLiteConnection SettingsConnection { get; set; } = new SQLiteConnection();

        public Player_NamesDAO()
        {
            string constr = "";
            constr = CommonUtils.getSettingsDBConnectionString();
            SettingsConnection.ConnectionString = constr;
        }
        public int AddFirstName(string FirstName)
        {
            int r = 0;
            SQLiteCommand cmd = null;
            try
            {
                SettingsConnection.Open();
                string sSQL = "INSERT INTO POTENTIAL_FIRST_NAMES VALUES(@FirstName)";

                cmd = SettingsConnection.CreateCommand();
                cmd.CommandText = sSQL;
                cmd.Parameters.Add("@FirstName", DbType.String).Value = FirstName;
                r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //do nothing
                String s = ex.Message;
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

        public int AddLastName(string LastName)
        {
            int r = 0;
            SQLiteCommand cmd = null;
            try
            {
                SettingsConnection.Open();
                string sSQL = "INSERT INTO POTENTIAL_LAST_NAMES VALUES(@LastName)";

                cmd = SettingsConnection.CreateCommand();
                cmd.CommandText = sSQL;
                cmd.Parameters.Add("@LastName", DbType.String).Value = LastName;
                r = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //do nothing
                String s = ex.Message;
            }
            // Do nothing
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }

            return r;
        }
        public long getTotalFirstNames()
        {
            long r = 0;
            SQLiteCommand cmd = null;
            try
            {
                SettingsConnection.Open();
                string sSQL = "SELECT COUNT(*) FROM POTENTIAL_FIRST_NAMES";
                cmd = SettingsConnection.CreateCommand();
                cmd.CommandText = sSQL;
                r = Convert.ToInt64(cmd.ExecuteScalar());
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

        public long getTotalLastNames()
        {
            long r = 0;
            SQLiteCommand cmd = null;
            try
            {
                SettingsConnection.Open();
                string sSQL = "SELECT COUNT(*) FROM POTENTIAL_LAST_NAMES";
                cmd = SettingsConnection.CreateCommand();
                cmd.CommandText = sSQL;
                r = Convert.ToInt64(cmd.ExecuteScalar());
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
        public string[] CreatePlayerName()
        {
            string sFirstName;
            string sLastName;
            string sSQL;
            SQLiteCommand cmd = null;
            try
            {
                SettingsConnection.Open();

                sSQL = "SELECT FirstName FROM POTENTIAL_FIRST_NAMES ORDER BY RANDOM() LIMIT 1;";
                cmd = SettingsConnection.CreateCommand();
                cmd.CommandText = sSQL;
                sFirstName = (String) cmd.ExecuteScalar();

                sSQL = "SELECT LastName FROM POTENTIAL_LAST_NAMES ORDER BY RANDOM() LIMIT 1;";
                cmd = SettingsConnection.CreateCommand();
                cmd.CommandText = sSQL;
                sLastName = (String) cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if ((int)SettingsConnection.State == (int)ConnectionState.Open)
                    SettingsConnection.Close();
            }

            return new string[] { sFirstName, sLastName };
        }
    }
}
