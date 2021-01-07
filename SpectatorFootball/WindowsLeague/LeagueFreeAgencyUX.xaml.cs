using log4net;
using SpectatorFootball.Free_Agency;
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
    /// Interaction logic for LeagueFreeAgencyUX.xaml
    /// </summary>
    public partial class LeagueFreeAgencyUX : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        //bindings for UI
        ObservableCollection<FreeAgencyTrans> FreeAgency_Pick_list = null;
        ObservableCollection<Player_and_Ratings> FreeAgency_Players_list = null;

        // pw is the parent window mainwindow
        private MainWindow pw;

        public event EventHandler Show_Standings;
        public LeagueFreeAgencyUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;

            lblFreeAgencyHeader.Content = pw.Loaded_League.season.Year.ToString() + " " +
            pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name + " Free Agency";

            FreeAgency_Services fas = new FreeAgency_Services();
            FreeAgency_Pick_list = new ObservableCollection<FreeAgencyTrans>(fas.GetFreeAgentTransList(pw.Loaded_League));

            FreeAgency_Players_list = new ObservableCollection<Player_and_Ratings>(fas.GetFreeAgentsList(pw.Loaded_League));

            lstPicks.ItemsSource = FreeAgency_Pick_list;
            lstFreeAgents.ItemsSource = FreeAgency_Players_list;

            if (isFreeAgencyDone())
                btnFreeAgency.IsEnabled = false;

        }
        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFreeAgency_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Beginning executing free agency at beginning of season.");
                List<long> fa_order = Team_Helper.getAllFranchiseIDThisSeason(pw.Loaded_League);
                fa_order = CommonUtils.ShufleList(fa_order);
                FreeAgency_Services fas = new FreeAgency_Services();
                while (fa_order.Count() > 0)
                {

                    for (int i = 0; i < fa_order.Count; i++)
                    {
                        FreeAgencyTrans fatrans = null;
                        Player_and_Ratings p = fas.Select_Free_Agent(pw.Loaded_League, fa_order[i], FreeAgency_Players_list.ToList(), ref fatrans);
                        if (p == null)
                        {
                            fa_order.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            FreeAgency_Players_list.Remove(p);
                            FreeAgency_Pick_list.Add(fatrans);
                        }

                        updateUI();
                    }
                }
                btnFreeAgency.IsEnabled = false;
                logger.Info("Ending executing free agency at beginning of season.");
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error executing free agency at beginning of season");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private bool isFreeAgencyDone()
        {
            bool r = false;
            //            FreeAgency_Helper fah = new FreeAgency_Helper();
            //            r = fah.isFreeAgencyDone(pw.Loaded_League);

            if (pw.Loaded_League.LState == Enum.League_State.FreeAgency_Started ||
                pw.Loaded_League.LState == Enum.League_State.Draft_Completed)
                r = false;
            else
                r = true;

            return r;
        }
        private void updateUI()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                new ThreadStart(delegate {
                    lstPicks.Items.Refresh();
                    lstFreeAgents.Items.Refresh();
                    lstPicks.SelectedItem = FreeAgency_Pick_list.Count() - 1;
                    lstPicks.ScrollIntoView(FreeAgency_Pick_list[(int)FreeAgency_Pick_list.Count() - 1]);
                }));

        }
    }

}
