using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Enum
{
    public enum Penalty_Play_Types
    {
        A, //All Plays
        AO, //All Offensive Plays
        PO, //Pass Offensive Plays
        RO, //Rush Offensive Plays
        AD, //All Defensive Plays
        PD, //Pass Defensive Plays
        PR, //Rush Defensive Plays
        KR, //kickoff returns
        KD, //kickoff defense
        PTR, //Punt returns
        PTD, //Punt defense
        FGO, //Field Goal Kicking Team
        FGD //Feild Goal Defending Team
    }
}
