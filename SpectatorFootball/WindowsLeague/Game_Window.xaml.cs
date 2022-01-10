using log4net;
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
    /// Interaction logic for Game_Window.xaml
    /// </summary>
    public partial class Game_Window : Window
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        private MainWindow pw;
        public Game_Window(MainWindow pw)
        {
            InitializeComponent();
            lblLeague.Content = pw.Loaded_League.season.League_Structure_by_Season[0].Long_Name;
        }

        private void hlp_nl_close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
