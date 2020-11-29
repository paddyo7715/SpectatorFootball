using log4net;
using SpectatorFootball.Enum;
using SpectatorFootball.Models;
using SpectatorFootball.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public LeagueDraftUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
            Draft_Services ds = new Draft_Services();
            Draft_Pick_list = new ObservableCollection<DraftPick>(ds.GetDraftList(pw.Loaded_League));
            Draft_Players_list = new ObservableCollection<Player>(ds.getDraftablePlayers(pw.Loaded_League));

            lstPicks.ItemsSource = Draft_Pick_list;
            lstDraftPlayers.ItemsSource = Draft_Players_list;

            if (!MorePicks())
                EndofDraft();
        }

        private void lstDraftPlayers_Click(object sender, RoutedEventArgs e)
        {
            ListView ls = (ListView)sender;

            if (ls.SelectedItems.Count > 0)
            {
                Player p = Draft_Players_list[ls.SelectedIndex];
                Draft_Profile_Popup dpp = new Draft_Profile_Popup(p);
                dpp.Top = (SystemParameters.PrimaryScreenHeight - dpp.Height) / 2;
                dpp.Left = (SystemParameters.PrimaryScreenWidth - dpp.Width) / 2;
                dpp.ShowDialog();
            }
        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Making single draft pick");
                disableDraftBTNS();
                int draft_rounds = Convert.ToInt32(Draft_Pick_list.Count / pw.Loaded_League.season.League_Structure_by_Season[0].Num_Teams);
                Draft_Services ds = new Draft_Services();
                DraftPick dp = Draft_Pick_list.Where(x => x.Pick_Pos_Name.Trim() == "").OrderBy(x => x.Pick_no).First();
                Player p = ds.Select_Draft_Pick(pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name, Draft_Players_list.ToList(), dp, draft_rounds);
                updatelists(dp.ID -1, p);
                lstPicks.Items.Refresh();
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
                EndofPicksorDraft();
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
                    DraftPick dp = Draft_Pick_list.Where(x => x.Pick_Pos_Name.Trim() == "").OrderBy(x => x.Pick_no).First();
                    Player p = ds.Select_Draft_Pick(pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name, Draft_Players_list.ToList(), dp, draft_rounds);
                    updatelists(dp.ID - 1, p);
                    lstPicks.Items.Refresh();
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
                EndofPicksorDraft();
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
                    DraftPick dp = Draft_Pick_list.Where(x => x.Pick_Pos_Name.Trim() == "").OrderBy(x => x.Pick_no).First();
                    Player p = ds.Select_Draft_Pick(pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name, Draft_Players_list.ToList(), dp, draft_rounds);
                    updatelists(dp.ID - 1, p);
                    lstPicks.Items.Refresh();
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
                EndofPicksorDraft();
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
        private void EndofPicksorDraft()
        {
            bool bMorePicks = false;
            bMorePicks = Draft_Pick_list.Any(x => x.Pick_Pos_Name.Trim() == "");
            if (bMorePicks)
                enableDraftBTNS();
            else
                EndofDraft();
        }
        private void updatelists(long draftpick_ind, Player p)
        {
            //            DraftPick d = Draft_Pick_list.Where(x => x.ID == dp.ID).First();
            //            d.Pick_Pos_Name = ((Player_Pos)p.Pos).ToString() + " " + p.First_Name + " " + p.Last_Name;
            int i = (int)draftpick_ind;
            Draft_Pick_list[i].Pick_Pos_Name = ((Player_Pos)p.Pos).ToString() + " " + p.First_Name + " " + p.Last_Name;
        }
    }
}
