using log4net;
using SpectatorFootball.Help_Forms;
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
    /// Interaction logic for ChampionsUX.xaml
    /// </summary>
    public partial class ChampionsUX : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        private MainWindow pw;

        public event EventHandler Show_Standings;

        public List<WeeklyScheduleRec> Champ_Game_List { get; set; }

        public ChampionsUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;

            long Season_ID = pw.Loaded_League.season.ID;
            string League_Shortname = pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name;
            long conferences = pw.Loaded_League.season.League_Structure_by_Season[0].Number_of_Conferences;
            long PlayoffTeams = pw.Loaded_League.season.League_Structure_by_Season[0].Num_Playoff_Teams;
            string ChampGameName = pw.Loaded_League.season.League_Structure_by_Season[0].Championship_Game_Name;

            Schedule_Services ss = new Schedule_Services();

            lblHeader.Content = pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name + " " +
                pw.Loaded_League.season.League_Structure_by_Season[0].Championship_Game_Name;

            Champ_Game_List = ss.getAlChampionshipGames(pw.Loaded_League);
            lstGames.ItemsSource = Champ_Game_List;
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            var help_form = new Help_Champions();
            help_form.Top = (SystemParameters.PrimaryScreenHeight - help_form.Height) / 2;
            help_form.Left = (SystemParameters.PrimaryScreenWidth - help_form.Width) / 2;
            help_form.ShowDialog();
        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void lstGames_Click(object sender, RoutedEventArgs e)
        {
            ListView ls = (ListView)sender;

            if (Mouse.OverrideCursor == Cursors.Wait ||
                (ls.SelectedItems.Count == 0)) return;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                WeeklyScheduleRec wsr = Champ_Game_List[ls.SelectedIndex];

            Game_Services gs = new Game_Services();

            if (wsr.Action == "") return;

            Game g = null;
            g = gs.geGamefromID(wsr.Game_ID, pw.Loaded_League);

                if (wsr.Action == "Game Summary")
                {
                    WeeklyScheduleRec wsched = Champ_Game_List[lstGames.SelectedIndex];
                    BoxScore bs_rec = gs.getGameandStatsfromID(wsched.Game_ID, pw.Loaded_League);

                    bool bPenalties = false;
                    long cur_season_id = pw.Loaded_League.AllSeasons.Where(x => x.Year == pw.Loaded_League.Current_Year).Select(x => x.Year).First();
                    long l = pw.Loaded_League.season.League_Structure_by_Season.Where(x => x.Season_ID == cur_season_id).Select(x => x.Penalties).First();
                    bPenalties = l == 1 ? true : false;

                    BoxScore_Popup dpp = new BoxScore_Popup(bs_rec, bPenalties);
                    dpp.Left = (SystemParameters.PrimaryScreenWidth - dpp.Width) / 2;
                    dpp.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Loading Game Box Score");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }
    }
}
