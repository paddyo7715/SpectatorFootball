using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.Help_Forms;
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
    /// Interaction logic for LeagueDraftUX.xaml
    /// </summary>
    public partial class LeagueDraftUX : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        //bindings for UI
        ObservableCollection<DraftPick> Draft_Pick_list = null;
        ObservableCollection<Player> Draft_Players_list = null;

        // pw is the parent window mainwindow
        private MainWindow pw;

        public event EventHandler Show_Standings;
        public event EventHandler Set_TopMenu;

        private Style announcerStyle = (Style)System.Windows.Application.Current.FindResource("DraftAnnouncementStyle");

        public LeagueDraftUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;

            lblDraftHeader.Content = pw.Loaded_League.season.Year.ToString() + " " +
                pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name + " Draft";

            Draft_Services ds = new Draft_Services();
            Draft_Pick_list = new ObservableCollection<DraftPick>(ds.GetDraftList(pw.Loaded_League));
            Draft_Players_list = new ObservableCollection<Player>(ds.getDraftablePlayers(pw.Loaded_League));

            lstPicks.ItemsSource = Draft_Pick_list;
            lstDraftPlayers.ItemsSource = Draft_Players_list;

            if (pw.Loaded_League.LState != League_State.Draft_Started &&
                pw.Loaded_League.LState != League_State.Season_Started)
                EndofDraft();
        }

        private void lstDraftPlayers_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;

            ListView ls = (ListView)sender;

            if (ls.SelectedItems.Count > 0)
            {
                Player p = Draft_Players_list[ls.SelectedIndex];
                Draft_Profile_Popup dpp = new Draft_Profile_Popup(p);
                dpp.Left = (SystemParameters.PrimaryScreenWidth - dpp.Width) / 2;
                dpp.ShowDialog();
            }
        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            var help_form = new Help_Draft();
            help_form.Top = (SystemParameters.PrimaryScreenHeight - help_form.Height) / 2;
            help_form.Left = (SystemParameters.PrimaryScreenWidth - help_form.Width) / 2;
            help_form.ShowDialog();
        }

        private void NextPick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Making single draft pick");
                disableDraftBTNS();
                System.Threading.Thread.Sleep(delayMilaSeconds());
                int draft_rounds = Convert.ToInt32(Draft_Pick_list.Count / pw.Loaded_League.season.League_Structure_by_Season[0].Num_Teams);
                Draft_Services ds = new Draft_Services();
                DraftPick dp = Draft_Pick_list.Where(x => x.Pick_Pos_Name.Trim() == "").OrderBy(x => x.Pick_no).First();
                Players_By_Team pbt = ds.Select_Draft_Pick(pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name, Draft_Players_list.ToList(), dp, draft_rounds);
                Player p = pbt.Player;
                updatelists(dp.ID -1, p);

                int pid = Draft_Players_list.IndexOf(p);
                Draft_Players_list[pid] = p;
                updateUI(dp, pid);

                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Making single draft pick");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (EndofPicksorDraft())
                    MessageBox.Show("Draft has Completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void simRound_Click(object sender, RoutedEventArgs e)
        {
            bool bMorePicks = true;
            Draft_Services ds = new Draft_Services();
            bool bFirst = true;
            long initial_round = 0;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Drafting round of picks");
                disableDraftBTNS();
                int draft_rounds = Convert.ToInt32(Draft_Pick_list.Count / pw.Loaded_League.season.League_Structure_by_Season[0].Num_Teams);
                do
                {
                    System.Threading.Thread.Sleep(delayMilaSeconds());
                    DraftPick dp = Draft_Pick_list.Where(x => x.Pick_Pos_Name.Trim() == "").OrderBy(x => x.Pick_no).First();
                    Players_By_Team pbt = ds.Select_Draft_Pick(pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name, Draft_Players_list.ToList(), dp, draft_rounds);
                    Player p = pbt.Player;
                    updatelists(dp.ID - 1, p);

                    int pid = Draft_Players_list.IndexOf(p);
                    Draft_Players_list[pid] = p;
                    updateUI(dp, pid);

                    if (bFirst)
                    {
                        initial_round = dp.Round;
                        bFirst = false;
                    }

                    if (!Draft_Pick_list.Any(x => x.Round == initial_round && x.Pick_Pos_Name.Trim() == ""))
                        bMorePicks = false;

                } while (bMorePicks);
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Making sim round draft picks");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (EndofPicksorDraft())
                    MessageBox.Show("Draft has Completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void simDraft_Click(object sender, RoutedEventArgs e)
        {
            bool bMorePicks = true;
            Draft_Services ds = new Draft_Services();

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Drafting round of picks");
                disableDraftBTNS();
                int draft_rounds = Convert.ToInt32(Draft_Pick_list.Count / pw.Loaded_League.season.League_Structure_by_Season[0].Num_Teams);
                do
                {
                    System.Threading.Thread.Sleep(delayMilaSeconds());
                    DraftPick dp = Draft_Pick_list.Where(x => x.Pick_Pos_Name.Trim() == "").OrderBy(x => x.Pick_no).First();
                    Players_By_Team pbt = ds.Select_Draft_Pick(pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name, Draft_Players_list.ToList(), dp, draft_rounds);
                    Player p = pbt.Player;
                    updatelists(dp.ID - 1, p);

                    int pid = Draft_Players_list.IndexOf(p);
                    Draft_Players_list[pid] = p;
                    updateUI(dp,pid);

                    if (!Draft_Pick_list.Any(x => x.Pick_Pos_Name.Trim() == ""))
                        bMorePicks = false;

                } while (bMorePicks);
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Making sim whole draft picks");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (EndofPicksorDraft())
                    MessageBox.Show("Draft has Completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private bool MorePicks()
        {
            bool r;

            int num_picks_left = Draft_Pick_list.Where(x => x.Pick_Pos_Name.Trim() == "").Count();

            if (num_picks_left == 0)
                r = false;
            else
                r = true;

            return r;
        }
        private void EndofDraft()
        {
            disableDraftBTNS();
            tckPickSpeed.IsEnabled = false;
        }
        private void disableDraftBTNS()
        {
            NextPick.IsEnabled = false;
            simRound.IsEnabled = false;
            simDraft.IsEnabled = false;
        }
        private void enableDraftBTNS()
        {
            NextPick.IsEnabled = true;
            simRound.IsEnabled = true;
            simDraft.IsEnabled = true;
        }
        private bool EndofPicksorDraft()
        {
            bool bMorePicks = false;
            bMorePicks = Draft_Pick_list.Any(x => x.Pick_Pos_Name.Trim() == "");
            if (bMorePicks)
                enableDraftBTNS();
            else
            {
                EndofDraft();
                League_Services ls = new League_Services();

                pw.Loaded_League.LState = ls.getSeasonState(true,
                     pw.Loaded_League.season.ID, pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name);
                Set_TopMenu?.Invoke(this, new EventArgs());
            }

            return !bMorePicks;
        }
        private void updatelists(long draftpick_ind, Player p)
        {
            int i = (int)draftpick_ind;
            Draft_Pick_list[i].Pick_Pos_Name = ((Player_Pos)p.Pos).ToString() + " " + p.First_Name + " " + p.Last_Name;
        }
        private void updateUI(DraftPick dp, int pid)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, 
                new ThreadStart(delegate { 
                    lstPicks.Items.Refresh(); 
                    lstPicks.SelectedItem = dp.ID - 1; 
                    lstPicks.ScrollIntoView(Draft_Pick_list[(int)dp.ID - 1]);

                    lstDraftPlayers.Items.Refresh();
                    lstDraftPlayers.SelectedItem = pid;
                    lstDraftPlayers.ScrollIntoView(Draft_Players_list[pid]);

                    txtAnnouncement.Text = "With the #" + dp.Pick_no + " pick in the " + pw.Loaded_League.season.Year + " " + pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name + " draft, the" + Environment.NewLine + 
                    dp.Team_Name + " have selected " + Environment.NewLine +  
                    dp.Pick_Pos_Name;

                    string[] s1 = Uniform.getTeamDispColors(dp.Home_Jersey_Color,
                          dp.Home_Jersey_Number_Color, dp.Home_Jersey_Outline_Color, dp.Helmet_Color, dp.Helmet_Logo_Color,dp.Home_Pants_Color);

                    txtAnnouncement.Background = CommonUtils.getBrushfromHexString(s1[0]);
                    txtAnnouncement.Foreground = CommonUtils.getBrushfromHexString(s1[1]);
                    brdAnnouncement.BorderBrush = CommonUtils.getBrushfromHexString(s1[2]);
                }));

        }
        private int delayMilaSeconds()
        {
            int r = 0;

            int speed = (int)tckPickSpeed.Value;
            switch (speed)
            {
                case 0:
                    r = 5000;
                    break;
                case 1:
                    r = 2500;
                    break;
                case 2:
                    r = 1000;
                    break;
                case 3:
                    r = 500;
                    break;
                case 4:
                    r = 0;
                    break;
            }


            return r;
        }
    }
}
