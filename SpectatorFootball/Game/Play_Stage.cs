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
        public bool Player_Catches_Ball = false;  //Player will catch ball so set his zindex 
        public List<Action> Actions = new List<Action>();
    }
}
