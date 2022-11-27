using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Play_Stage
    {
        //When this player or ball is the main object, them ending ttheir action ends the current stage.
        public bool Main_Object;
        public List<Action> Actions = new List<Action>();
    }
}
