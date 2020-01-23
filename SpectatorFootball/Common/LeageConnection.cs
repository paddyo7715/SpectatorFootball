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
        private static LeageConnection _ConsString = null;
        private String _String = null;

        public static string ConString
        {
            get
            {
                if (_ConsString == null)
                {
                    _ConsString = new LeageConnection { _String = LeageConnection.Connect() };
                    return _ConsString._String;
                }
                else
                    return _ConsString._String;
            }
        }

        public static string Connect()
        {

            string r = "";

            string Provider = null;
            string metadata = null;
            string connectionString = ConfigurationManager.ConnectionStrings["mainEntities"].ConnectionString;

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
                ProviderConnectionString = CommonUtils.getSettingsDBConnectionString(),
            };

            r = entityString.ToString();

            return r;


        }



    }
}
