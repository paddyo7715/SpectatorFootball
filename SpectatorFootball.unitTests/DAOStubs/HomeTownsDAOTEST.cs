using SpectatorFootball.DAO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.unitTests.DAOStubs
{
    class HomeTownsDAOTEST : IHomeTownsDAO
    {
        public int getTotalHomeTowns()
        {
            return 10;
        }
        public string getHomeTownbyRecNum(int rec_num)
        {
            string sHometown = null;

            List<string> hTownList = new List<string>
            {
                "Albany, NY", "Trenton, NJ", "LA, California", "Reno, LV", "Miami, FL",
                "Mobile, AL", "Dallas, TX", "Houston, TX", "Juno, Alaska", "Seattle, WAS"
            };

            int hint = CommonUtils.getRandomIndex(hTownList.Count());

            sHometown = hTownList[hint];

            return sHometown;
        }
    }
}
