using log4net;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for LeagueInjuriesUX.xaml
    /// </summary>
    public partial class LeagueInjuriesUX : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");
        private Injuries_Services iserv = new Injuries_Services();

        // pw is the parent window mainwindow
        private MainWindow pw;
        public event EventHandler Show_Standings;
        public LeagueInjuriesUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
            List<League_Injuries> League_Injuries = null;
            League_Injuries = iserv.GetLeagueInjuredPlayers(pw.Loaded_League);
            lstInjuries.ItemsSource = League_Injuries;
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }
    }
}
