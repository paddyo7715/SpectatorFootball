using SpectatorFootball.Common;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for Game_Options_Dialog.xaml
    /// </summary>
    public partial class Game_Options_Dialog : System.Windows.Window
    {
        private string ball_Color1;
        private string ball_Color2;

        public ObservableCollection<Football_Color_rec> ball_color_list { get; set; }
        public Game_Options_Dialog(string ball_Colors, bool bCrowdNoise, bool bFootballSounds, string GameSpeed)
        {
            InitializeComponent();

            this.DataContext = this;
            ball_color_list = new ObservableCollection<Football_Color_rec>(League_Helper.getBallColors());
            string[] m = ball_Colors.Split('|');
            ball_Color1 = m[0];
            ball_Color2 = m[1];
            int selectedIndex = 0;

            var cboItems = cboBallColor.Items;
            int i = 0;
            foreach (Football_Color_rec c in ball_color_list)
            {
                if (c.Color1.ToUpper() == ball_Color1.ToUpper() &&
                    c.Color2.ToUpper() == ball_Color2.ToUpper())
                {
                    selectedIndex = i;
                    break;
                }
                i++;
            }

            cboBallColor.SelectedIndex = selectedIndex;

            chbCrowdNoise.IsChecked = bCrowdNoise;
            chbFootballSounds.IsChecked = bFootballSounds;

            RadioButton rb = null;
            GameSpeed = GameSpeed.Trim();
            if (GameSpeed == "FF")
                rb = optGSFastest;
            else if (GameSpeed == "F")
                rb = optGSFast;
            else if (GameSpeed == "N")
                rb = optGSNormal;
            else if (GameSpeed == "S")
                rb = optGSSlow;
            else if (GameSpeed == "SS")
                rb = optGSSlowest;

            rb.IsChecked = true;



        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
           Football_Color_rec fcr = (Football_Color_rec)cboBallColor.SelectedItem;

            string color_string = fcr.Color1 + "|" + fcr.Color2;

            bool bCrowdNoise = (bool)chbCrowdNoise.IsChecked;
            bool bFootballSounds = (bool)chbFootballSounds.IsChecked;

            RadioButton rb = null;
            if (optGSFastest.IsChecked == true)
                rb = optGSFastest;
            else if (optGSFast.IsChecked == true)
                rb = optGSFast;
            else if (optGSNormal.IsChecked == true)
                rb = optGSNormal;
            else if (optGSSlow.IsChecked == true)
                rb = optGSSlow;
            else if (optGSSlowest.IsChecked == true)
                rb = optGSSlowest;

            string GameSpeed = rb.Tag.ToString().Trim();

            League_Services ls = new League_Services();
            ls.Create_Options_File(color_string, bCrowdNoise, bFootballSounds, GameSpeed);
            this.Close();
        }

    }
}
