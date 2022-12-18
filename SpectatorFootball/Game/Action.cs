using SpectatorFootball.Common;
using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.GameNS
{
    public class Action
    {
        public Game_Object_Types type;
        public double start_yardline;
        public double start_vertical;
        public double end_yardline;
        public double end_vertical;
        public bool bPossesses_Ball;
        public bool bEndofStageAction;
        public Player_States? p_state;
        public Ball_States? b_state;
        public Game_Sounds? Sound;
        public Movement? MoveType;
        public List<PointXY> PointXY = new List<PointXY>();

        public Action(Game_Object_Types type, double start_yardline, double start_vertical,
         double end_yardline, double end_vertical, bool bPossesses_Ball,
         bool bEndofStageAction, Player_States? p_state, Ball_States? b_state,
         Game_Sounds? Sound, Movement? MoveType)
        {
            this.type = type;
            this.start_yardline = start_yardline;
            this.start_vertical = start_vertical;
            this.end_yardline = end_yardline;
            this.end_vertical = end_vertical;
            this.bPossesses_Ball = bPossesses_Ball;
            this.bEndofStageAction = bEndofStageAction;
            this.p_state = p_state;
            this.b_state = b_state;
            this.Sound = Sound;
            this.MoveType = MoveType;

            bool bBall = false;

            if (type == Game_Object_Types.B)
                bBall = true;

            switch (MoveType)
            {
                case Movement.LINE:
                    PointXY = PointPlotter.PlotLine(bBall, start_yardline, start_vertical, end_yardline, end_vertical, bEndofStageAction);
                    break;
            }
        }

    }
}
