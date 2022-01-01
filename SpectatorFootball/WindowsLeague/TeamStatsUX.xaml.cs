using log4net;
using SpectatorFootball.Models;
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
    /// Interaction logic for TeamStatusUX.xaml
    /// </summary>
    public partial class TeamStatusUX : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        // pw is the parent window mainwindow
        private MainWindow pw;
        private List<Team_Stat_Rec> teamStatsList = null;
        private string sorted_field = "";
        private bool bDescending = false;

        private League_Services lServices = new League_Services();

        public event EventHandler Show_Standings;
        public TeamStatusUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
            lblHeader.Content = "Team Status " + pw.Loaded_League.season.Year;
            sorted_field = "Rating";
            bDescending = true;
            League_Services ls = new League_Services();
            teamStatsList = ls.getLeagueStats(pw.Loaded_League, teamStatsList, sorted_field, bDescending);
            lstTeamStats.ItemsSource = teamStatsList;

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
