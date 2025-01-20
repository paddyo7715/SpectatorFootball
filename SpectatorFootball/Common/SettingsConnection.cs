using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Common
{
    public class SettingsConnection

    {
        private SettingsConnection() { }
        private static SettingsConnection _ConsString = null;
        private String _String = null;

        public static string ConString
        {
            get
            {
                if (_ConsString == null)
                {
                    _ConsString = new SettingsConnection { _String = SettingsConnection.Connect() };
                    return _ConsString._String;
                }
                else
                    return _ConsString._String;
            }
        }

        public static string Connect()
        {

            string r = "";

            r = CommonUtils.getSettingsDBConnectionString();

            return r;


        }
    }
}
