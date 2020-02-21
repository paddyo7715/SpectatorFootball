using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpectatorFootball.Versioning
{

    class App_Version
    {
        public static readonly string APP_VERSION = "1.0.0";

        public  bool isCompatibleVersion(string dbVersion)
        {
            bool r = false;

            switch (APP_VERSION)
            {
                case "1.0.0":
                    if (dbVersion == "1.0.0")
                        r = true;
                    else
                        r = false;
                    break;
            }

            return r;
        }
    }
}
