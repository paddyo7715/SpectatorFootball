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
        private string ball_Colors;

        public Game_Options_Dialog(string ball_Colors)
        {
            InitializeComponent();
            this.ball_Colors = ball_Colors;
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
