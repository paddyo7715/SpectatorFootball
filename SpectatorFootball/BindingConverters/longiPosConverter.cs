using SpectatorFootball.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpectatorFootball.BindingConverters
{
    class longiPosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string r = "";

            if (value != null)
            {
                int ipos = int.Parse(value.ToString());
                Player_Pos pos = (Player_Pos)ipos;

                switch (pos)
                {
                    case Player_Pos.QB:
                        {
                            r = "Quarterback";
                            break;
                        }

                    case Player_Pos.RB:
                        {
                            r = "Running Back";
                            break;
                        }

                    case Player_Pos.WR:
                        {
                            r = "Wide Receiver";
                            break;
                        }

                    case Player_Pos.TE:
                        {
                            r = "Tight End";
                            break;
                        }

                    case Player_Pos.OL:
                        {
                            r = "Offensive Lineman";
                            break;
                        }

                    case Player_Pos.DL:
                        {
                            r = "Defensive Lineman";
                            break;
                        }

                    case Player_Pos.LB:
                        {
                            r = "Linebacker";
                            break;
                        }

                    case Player_Pos.DB:
                        {
                            r = "Defensive Back";
                            break;
                        }

                    case Player_Pos.K:
                        {
                            r = "Kicker";
                            break;
                        }

                    case Player_Pos.P:
                        {
                            r = "Punter";
                            break;
                        }
                }
            }

            return r;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            int r = 0;

            return r;

        }
    }
}
