using log4net;
using SpectatorFootball.Help_Forms;
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
        string last_sort_stat = "";
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
            var help_form = new Help_TeamStats();
            help_form.Top = (SystemParameters.PrimaryScreenHeight - help_form.Height) / 2;
            help_form.Left = (SystemParameters.PrimaryScreenWidth - help_form.Width) / 2;
            help_form.ShowDialog();
        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }

        public void ListViewHeader_Click(object sender,
                                RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
   
            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    sorted_field = headerClicked.Column.Header.ToString();
                    if (sorted_field == last_sort_stat)
                    {
                        bDescending = bDescending == true ? false : true;
                    }
                    else
                    {
                        bDescending = true;
                    }
                }
                League_Services ls = new League_Services();
                teamStatsList = ls.getLeagueStats(pw.Loaded_League, teamStatsList, sorted_field, bDescending);
                lstTeamStats.ItemsSource = teamStatsList;
                last_sort_stat = sorted_field;
            }
        }
    }
}
