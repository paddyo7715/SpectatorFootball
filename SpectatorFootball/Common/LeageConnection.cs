using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Common
{
    class LeageConnection
    {
        private LeageConnection() { }

        public static string Connect(string leaguepath)
        {

            string r = "";

            string Provider = null;
            string metadata = null;
            string connectionString = ConfigurationManager.ConnectionStrings["leagueContext"].ConnectionString;

            string[] m = null;
            m = connectionString.Split(';');
            foreach (string x in m)
            {
                if (x.StartsWith("metadata="))
                {
                    metadata = x.Split('=')[1];
                }
                else if (x.StartsWith("provider="))
                {
                    Provider = x.Split('=')[1];
                }
            }

            EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
            {
                Provider = Provider,
                Metadata = metadata,
                ProviderConnectionString = CommonUtils.getLeagueDBConnectionString(leaguepath),
            };

            r = entityString.ToString();

            return r;


        }



    }
}
