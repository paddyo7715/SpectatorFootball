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

        public  int isCompatibleVersion(string dbVersion)
        {
            int r = 0;

            //r=0 then program and db are compatible
            //r=1 they are not compatible, but the db can be upgraded.
            //r=2 they are not compatible and the db can not be upgraded.

            switch (APP_VERSION)
            {
                case "1.0.0":
                    if (dbVersion == "1.0.0")
                        r = 0;
                    else
                        r = 1;
                    break;
            }

            return r;
        }
    }
}
