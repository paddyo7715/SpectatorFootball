using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for PlayerCard_Popup.xaml
    /// </summary>
    public partial class PlayerCard_Popup : Window
    {
        Player_Card_Data pcd = null;
        public PlayerCard_Popup(Player_Card_Data pcd)
        {
            InitializeComponent();
            string[]  m = Uniform.getTeamDispColors(pcd.team.Home_jersey_Color,
                pcd.team.Home_Jersey_Number_Color,
                pcd.team.Home_Jersey_Number_Outline_Color,
                pcd.team.Helmet_Color,
                pcd.team.Helmet_Logo_Color,
                pcd.team.Home_Pants_Color);

            pcName.Content = (Player_Pos)pcd.Player.Pos + " " + pcd.Player.First_Name + " " + pcd.Player.Last_Name;
            brdCard.BorderBrush = new SolidColorBrush(CommonUtils.getColorfromHex(m[2]));

            lblName.Content = pcName.Content;
            lblTeam.Content = pcd.team.Nickname;

            lblName.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(m[1]));
            lblTeam.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(m[1]));

            lblName.Background = new SolidColorBrush(CommonUtils.getColorfromHex(m[0]));
            lblTeam.Background = new SolidColorBrush(CommonUtils.getColorfromHex(m[0]));

            imgPlayerFace.Source = new BitmapImage(new Uri(CommonUtils.getAppPath() + "/Images/fplayer.jpg"));

            this.pcd = pcd;
            this.DataContext = this.pcd;
        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
