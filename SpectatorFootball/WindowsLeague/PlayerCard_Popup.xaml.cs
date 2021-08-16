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
        public PlayerCard_Popup(Player_Card_Data pcd)
        {
            InitializeComponent();
            pcName.Content = (Player_Pos)pcd.Player.Pos + " " + pcd.Player.First_Name + " " + pcd.Player.Last_Name;
        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
