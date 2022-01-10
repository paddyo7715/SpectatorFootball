using log4net;
using SpectatorFootball.Help_Forms;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
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
        private List<League_Injuries> League_Injuries = null;

        // pw is the parent window mainwindow
        private MainWindow pw;
        public event EventHandler Show_Standings;
        public LeagueInjuriesUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
            League_Injuries = iserv.GetLeagueInjuredPlayers(pw.Loaded_League);
            lstInjuries.ItemsSource = League_Injuries;
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            var help_form = new Help_LeagueInjuries();
            help_form.Top = (SystemParameters.PrimaryScreenHeight - help_form.Height) / 2;
            help_form.Left = (SystemParameters.PrimaryScreenWidth - help_form.Width) / 2;
            help_form.ShowDialog();
        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void lstInjuries_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;

            try
            {
                ListView ls = (ListView)sender;

                if (ls.SelectedItems.Count > 0)
                {
                    Player_Services ps = new Player_Services();

                    League_Injuries pr = League_Injuries[ls.SelectedIndex];
                    Player_Card_Data pcd = ps.getPlayerCardData(pr.p, pw.Loaded_League);
                    PlayerCard_Popup pcp = new PlayerCard_Popup(pcd);
                    pcp.Left = (SystemParameters.PrimaryScreenWidth - pcp.Width) / 2;
                    pcp.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Opening Player Profile Popup");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
