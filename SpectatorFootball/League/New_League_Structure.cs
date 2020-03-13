using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpectatorFootball.Models;

//This class is used to hold the structure of the new league before it is saved to the database.
//This needs to be done since the league database file is not even created at this point.
namespace SpectatorFootball.League
{
    public class New_League_Structure
    {
        public SpectatorFootball.Models.League League { get; set; }
        public DBVersion DBVersion { get; set; }

    }
}
