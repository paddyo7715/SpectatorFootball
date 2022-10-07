using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace SpectatorFootball.GameNS
{
    class Player_Graphics1_Rec
    {
        public Rectangle Player_Rect;
        public Player_Graphic_Sprite Graphic_Sprinte;
        public bool bHasBall = false;

        public static explicit operator int(Player_Graphics1_Rec v)
        {
            throw new NotImplementedException();
        }
    }
}
