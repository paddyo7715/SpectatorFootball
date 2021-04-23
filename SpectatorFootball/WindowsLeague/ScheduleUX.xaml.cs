using log4net;
using SpectatorFootball.Models;
using SpectatorFootball.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for ScheduleUX.xaml
    /// </summary>
    public partial class ScheduleUX : UserControl
    {
        //bindings for UI
        public ObservableCollection<Sched_Week_With_Name> Schedule_Weeks_List { get; set; }
        public ObservableCollection<WeeklyScheduleRec> Weekly_Sched_List { get; set; }

        private static ILog logger = LogManager.GetLogger("RollingFile");

        private MainWindow pw;

        public event EventHandler Show_Standings;
        public event EventHandler Set_TopMenu;
        public ScheduleUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;

            long Season_ID = pw.Loaded_League.season.ID;
            string League_Shortname = pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name;
            long conferences = pw.Loaded_League.season.League_Structure_by_Season[0].Number_of_Conferences;
            long PlayoffTeams = pw.Loaded_League.season.League_Structure_by_Season[0].Num_Playoff_Teams;
            string ChampGameName = pw.Loaded_League.season.League_Structure_by_Season[0].Championship_Game_Name;

            Schedule_Services ss = new Schedule_Services();

            lblScheduleHeader.Content = pw.Loaded_League.season.Year.ToString() + " " +
            pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name + " Schedule";

            Schedule_Weeks_List = new ObservableCollection<Sched_Week_With_Name>(ss.GetSchedWeeks(Season_ID, League_Shortname, conferences, PlayoffTeams, ChampGameName));

            Sched_Week_With_Name swwn = Schedule_Weeks_List.Where(x => x.Current_Week == true).First();
            cboWeek.SelectedItem = swwn;

            DataContext = this;
        }
        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());

        }
        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCurrentWeek_Click(object sender, RoutedEventArgs e)
        {
            Sched_Week_With_Name sw = Schedule_Weeks_List.Where(x => x.Current_Week == true).First();
            cboWeek.SelectedItem = sw;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            cboWeek.SelectedIndex += -1;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            cboWeek.SelectedIndex += 1;
        }

        private void cboWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;

            if (cboWeek.SelectedValue != null)
            {
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;

                Sched_Week_With_Name sw = (Sched_Week_With_Name)cboWeek.SelectedItem;
                lblWeekName.Content = sw.sWeek;

                Sched_Week_With_Name swCurrent = Schedule_Weeks_List.Where(x => x.Current_Week == true).First();
                if (sw.iWeek == swCurrent.iWeek)
                    btnCurrentWeek.IsEnabled = false;
                else
                    btnCurrentWeek.IsEnabled = true;

                long iWeek = cboWeek.SelectedIndex;
                if (iWeek == 0)
                    btnPrev.IsEnabled = false;

                if (iWeek == Schedule_Weeks_List.Count() - 1)
                    btnNext.IsEnabled = false;

                Schedule_Services ss = new Schedule_Services();
                Weekly_Sched_List = new ObservableCollection<WeeklyScheduleRec>(ss.getWeeklySched(pw.Loaded_League, sw.iWeek, swCurrent.iWeek));
                lstGames.ItemsSource = Weekly_Sched_List;

            }

        }
        private void lstGames_Click(object sender, RoutedEventArgs e)
        {
            ListView ls = (ListView)sender;

            if (Mouse.OverrideCursor == Cursors.Wait ||
                (ls.SelectedItems.Count == 0)) return;

            WeeklyScheduleRec wsr = Weekly_Sched_List[ls.SelectedIndex];

            Game_Services gs = new Game_Services();

            if (wsr.Action == "") return;

            Game g = null;
            g = gs.geGamefromID(wsr.Game_ID, pw.Loaded_League);

            if (wsr.Action == "Game Summary")
            {

            }
            else
            {
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    if (wsr.Action == "Play")
                    {
                        //pass the game object to a popup modal that plays the game and updates
                        //the passed in game object.
                    }
                    else //Resume
                    {
                        //Here I need to load the game in progress.  I'm not sure how to do that
                        //but I will need the game_id.  I think maybe store the serialized game
                        //object in the database on a key of game id
                    }
                    gs.SaveGame(g, pw.Loaded_League);
                }
                catch (Exception ex)
                {
                    Mouse.OverrideCursor = null;
                    logger.Error("Error Saving Game");
                    logger.Error(ex);
                    MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }




        }

    }
}
