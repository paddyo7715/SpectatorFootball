using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball
{
    class HomeTownsDAO
    {

        public int getTotalHomeTowns()
        {
            int r = 0;

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                //               context.Database.Log = Console.Write;
                r = context.HomeTowns.Count();
            }

            return r;
        }
        public string getHomeTownbyRecNum(int rec_num)
        {
            string r = null;

            string con = Common.SettingsConnection.Connect();
            using (var context = new settingsContext(con))
            {
                string HomeTown = context.HomeTowns.Where(z => z.ID == rec_num).Select(x => x.State + ", " + x.State).FirstOrDefault();
            }

            return r;
        }

    }
}
