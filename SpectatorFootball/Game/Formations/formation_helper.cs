using SpectatorFootball.Enum;
using SpectatorFootball.GameNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class formation_helper
    {
        public static Formation getFormation(Formations_Enum fe, double PossessionAdjuster)
        {
            iFormation_Play formplay = null;
            Formation f = null;

            switch (fe)
            {
                case Formations_Enum.KICKOFF_REGULAR_KICK:
                    formplay = new Kickoff_Classic_Formation();
                    break;
                case Formations_Enum.KICKOFF_REGULAR_RECEIVE:
                    formplay = new Kickoff_Classic_Return_Formation();
                    break;
                case Formations_Enum.KICKOFF_DYNAMIC_KICK:
                    formplay = new Kickoff_Dynamic_Formation();
                    break;
                case Formations_Enum.KICKOFF_DYNAMIC_RECEIVE:
                    formplay = new Kickoff_Dynamic_Return_Formation();
                    break;
                case Formations_Enum.KICKOFF_ONSIDE_KICK:
                    formplay = new Onside_Kickoff();
                    break;
                case Formations_Enum.KICKOFF_ONSIDE_RECEIVE:
                    formplay = new Onside_Kickoff_Receive();
                    break;
                case Formations_Enum.PUNT:
                    formplay = new Punt_Formation();
                    break;
                case Formations_Enum.PUNT_GL:
                    formplay = new Punt_Goalline_Formation();
                    break;
                case Formations_Enum.PUNT_RETURN:
                    formplay = new Punt_Return_Formation();
                    break;
            }

            f = formplay.getFormation(fe, PossessionAdjuster);

            return f;
        }
    }
}
