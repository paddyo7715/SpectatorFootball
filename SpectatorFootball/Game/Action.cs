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
        public Ball_Speed? Ball_Speed;
        public bool bnoSkip;
        public int Fake_Movement_Points;

        public Action(Game_Object_Types type, double start_yardline, double start_vertical,
         double end_yardline, double end_vertical, bool bPossesses_Ball,
         Player_States? p_state, Ball_States? b_state,
         Game_Sounds? Sound, Movement? MoveType, Ball_Speed? Ball_Speed, bool bnoSkip, int fake_Movement_Points)
        {
            this.type = type;
            this.start_yardline = start_yardline;
            this.start_vertical = start_vertical;
            this.end_yardline = end_yardline;
            this.end_vertical = end_vertical;
            this.bPossesses_Ball = bPossesses_Ball;
            this.p_state = p_state;
            this.b_state = b_state;
            this.Sound = Sound;
            this.MoveType = MoveType;
            this.Ball_Speed = Ball_Speed;
            this.bnoSkip = bnoSkip;
            this.Fake_Movement_Points = fake_Movement_Points;


            bool bBall = false;

            if (type == Game_Object_Types.B)
                bBall = true;

            switch (MoveType)
            {
                case Movement.LINE:
                    PointXY = PointPlotter.PlotLine(bBall, start_yardline, start_vertical, end_yardline, end_vertical, bEndofStageAction, Ball_Speed, b_state, bnoSkip);
                    break;
                case Movement.FAKE_MOVEMENT:
                    PointXY = PointPlotter.PlotFakeMoves(start_yardline, start_vertical, Fake_Movement_Points);
                    break;
            }

        }

    }
}
