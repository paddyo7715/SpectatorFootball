using SpectatorFootball.DAO.Interfaces;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.unitTests.DAOStubs
{
    public class Player_NamesDAOTEST : IPlayer_NamesDAO
    {
        public int AddFirstName(List<Potential_First_Names> FirstNames)
        {
            return 0;
        }

        public int AddLastName(List<Potential_Last_Names> LastNames)
        {
            return 0;
        }
        public long getTotalFirstNames()
        {
            return 0;
        }

        public long getTotalLastNames()
        {
            return 0;
        }
        public string[] CreatePlayerName()
        {
            string sFirstName = null;
            string sLastName = null;

            List<string> fName = new List<string>
            {
                "Bill", "Mike", "Dave", "Dan", "John",
                "Frank", "Chris", "George", "Lou", "Peter"
            };


            List<string> lName = new List<string>
            {
                "Miller", "Osgood", "OReilly", "Poole", "Patino",
                "Adams", "Dickson", "Marino", "Simms", "Beasly"
            };

            int fint = CommonUtils.getRandomIndex(fName.Count());
            int lint = CommonUtils.getRandomIndex(lName.Count());

            sFirstName = fName[fint];
            sLastName = lName[lint];

            return new string[] { sFirstName, sLastName };
        }
    }
}
