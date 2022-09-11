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
    /// Interaction logic for Game_Options_Dialog.xaml
    /// </summary>
    public partial class Game_Options_Dialog : Window
    {
        private string ball_Color1;
        private string ball_Color2;
        public Game_Options_Dialog(string ball_Colors)
        {
            InitializeComponent();
            string[] m = ball_Colors.Split('|');
            ball_Color1 = m[0];
            ball_Color2 = m[1];
            int selectedIndex = 0;

            var cboItems = cboBallColor.Items;
            int i = 0;
            foreach (ComboBoxItem cb in cboItems)
            {
                StackPanel sp = (StackPanel)cb.Content;
                Ellipse e = (Ellipse)sp.Children[0];
                var gradb = (LinearGradientBrush)e.Fill;
                string color1 = CommonUtils.getHexfromColor(gradb.GradientStops[0].Color);
                string color2 = CommonUtils.getHexfromColor(gradb.GradientStops[1].Color);
                if (color1.ToUpper() == ball_Color1.ToUpper() &&
                    color2.ToUpper() == ball_Color2.ToUpper())
                {
                    selectedIndex = i;
                    break;
                }
                i++;
            }

            cboBallColor.SelectedIndex = selectedIndex;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
