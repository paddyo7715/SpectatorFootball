using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.Help_Forms;
using SpectatorFootball.League;
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
    /// Interaction logic for StatsUX.xaml
    /// </summary>
    public partial class StatsUX : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        // pw is the parent window mainwindow
        private MainWindow pw;

        private League_Stats LeagueStats = new League_Stats();
        private Stat_Type sType;
        private Stat_Type sType_sorted;
        string last_sort_stat = "";
        bool Stat_Descend = true;
        private League_Services lServices = new League_Services();

        public event EventHandler Show_Standings;
        public StatsUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
            lblStatsHeader.Content = "Player Status " + pw.Loaded_League.season.Year;
            sType = Stat_Type.PASSING;
            last_sort_stat = "QBR";
            Stat_Descend = true;
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
            string sort_stat = null;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    sort_stat = headerClicked.Column.DisplayMemberBinding.ToString();
                    if (sType == sType_sorted && sort_stat == last_sort_stat)
                    {
                        Stat_Descend = Stat_Descend == true ? false : true;
                    }
                    else
                    {
                        Stat_Descend = true;
                    }
                }
                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.PASSING, last_sort_stat, Stat_Descend);
                setStatsList();
                sType_sorted = sType;
                last_sort_stat = sort_stat;
            }
        }

        private void setStatsList()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, sType, last_sort_stat, Stat_Descend);

                switch (sType)
                {
                case Stat_Type.PASSING:
                    lstPassing.ItemsSource = LeagueStats.Passing_Stats;
                    if (LeagueStats.Passing_Stats == null || LeagueStats.Passing_Stats.Count() == 0)
                        lblPassNoStats.Visibility = Visibility.Visible;
                    else
                        lblPassNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.RUSHING:
                    lstRushing.ItemsSource = LeagueStats.Rushing_Stats;
                    if (LeagueStats.Rushing_Stats == null || LeagueStats.Rushing_Stats.Count() == 0)
                        lblRushNoStats.Visibility = Visibility.Visible;
                    else
                        lblRushNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.RECEIVING:
                    lstReceiving.ItemsSource = LeagueStats.Receiving_Stats;
                    if (LeagueStats.Receiving_Stats == null || LeagueStats.Receiving_Stats.Count() == 0)
                        lblReceiveNoStats.Visibility = Visibility.Visible;
                    else
                        lblReceiveNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.BLOCKING:
                    lstBlocking.ItemsSource = LeagueStats.Blocking_Stats;
                    if (LeagueStats.Blocking_Stats == null || LeagueStats.Blocking_Stats.Count() == 0)
                        lblBlockNoStats.Visibility = Visibility.Visible;
                    else
                        lblBlockNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.DEFENSE:
                    lstDefense.ItemsSource = LeagueStats.Defense_Stats;
                    if (LeagueStats.Defense_Stats == null || LeagueStats.Defense_Stats.Count() == 0)
                        lblDefenseNoStats.Visibility = Visibility.Visible;
                    else
                        lblDefenseNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.PASS_DEFENSE:
                    lstPassDefense.ItemsSource = LeagueStats.Pass_Defense_Stats;
                    if (LeagueStats.Pass_Defense_Stats == null || LeagueStats.Pass_Defense_Stats.Count() == 0)
                        lblPassDefenseNoStats.Visibility = Visibility.Visible;
                    else
                        lblPassDefenseNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.KICKING:
                    lstFGKicking.ItemsSource = LeagueStats.Kicking_Stats;
                    if (LeagueStats.Kicking_Stats == null || LeagueStats.Kicking_Stats.Count() == 0)
                        lblFGKickingNoStats.Visibility = Visibility.Visible;
                    else
                        lblFGKickingNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.PUNTING:
                    lstPunting.ItemsSource = LeagueStats.Punting_Stats;
                    if (LeagueStats.Punting_Stats == null || LeagueStats.Punting_Stats.Count() == 0)
                        lblPuntingNoStats.Visibility = Visibility.Visible;
                    else
                        lblPuntingNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.KICK_RETURNS:
                    lstKickoffReturns.ItemsSource = LeagueStats.KickRet_Stats;
                    if (LeagueStats.KickRet_Stats == null || LeagueStats.KickRet_Stats.Count() == 0)
                        lblKickReturnsNoStats.Visibility = Visibility.Visible;
                    else
                        lblKickReturnsNoStats.Visibility = Visibility.Collapsed;
                    break;
                case Stat_Type.PUNT_RETURNS:
                    lstPuntReturns.ItemsSource = LeagueStats.PuntRet_Stats;
                    if (LeagueStats.PuntRet_Stats == null || LeagueStats.PuntRet_Stats.Count() == 0)
                        lblPuntReturnsNoStats.Visibility = Visibility.Visible;
                    else
                        lblPuntReturnsNoStats.Visibility = Visibility.Collapsed;
                    break;
                }
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Getting Player Stats");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void statsTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabPassing.IsSelected)
            {
                sType = Stat_Type.PASSING;
                last_sort_stat = "QBR";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.PASSING, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabRushing.IsSelected)
            {
                sType = Stat_Type.RUSHING;
                last_sort_stat = "Yards";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.RUSHING, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabReceiving.IsSelected)
            {
                sType = Stat_Type.RECEIVING;
                last_sort_stat = "Catches";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.RECEIVING, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabBlocking.IsSelected)
            {
                sType = Stat_Type.BLOCKING;
                last_sort_stat = "Pancakes";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.BLOCKING, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabDefense.IsSelected)
            {
                sType = Stat_Type.DEFENSE;
                last_sort_stat = "Sacks";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.DEFENSE, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabPassDefense.IsSelected)
            {
                sType = Stat_Type.PASS_DEFENSE;
                last_sort_stat = "Ints";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.PASS_DEFENSE, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabKicking.IsSelected)
            {
                sType = Stat_Type.KICKING;
                last_sort_stat = "FG_Made";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.KICKING, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabPunting.IsSelected)
            {
                sType = Stat_Type.PUNTING;
                last_sort_stat = "Punt_AVG";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.PUNTING, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabKickReturn.IsSelected)
            {
                sType = Stat_Type.KICK_RETURNS;
                last_sort_stat = "Yards_avg";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.KICK_RETURNS, last_sort_stat, Stat_Descend);
                setStatsList();
            }
            else if (tabPuntReturn.IsSelected)
            {
                sType = Stat_Type.PUNT_RETURNS;
                last_sort_stat = "Yards_avg";
                Stat_Descend = true;
//                LeagueStats = lServices.getSeasonStats(pw.Loaded_League, LeagueStats, Stat_Type.PUNT_RETURNS, last_sort_stat, Stat_Descend);
                setStatsList();
            }



        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            var help_form = new Help_PlayerStats();
            help_form.Top = (SystemParameters.PrimaryScreenHeight - help_form.Height) / 2;
            help_form.Left = (SystemParameters.PrimaryScreenWidth - help_form.Width) / 2;
            help_form.ShowDialog();

        }
    }
}
