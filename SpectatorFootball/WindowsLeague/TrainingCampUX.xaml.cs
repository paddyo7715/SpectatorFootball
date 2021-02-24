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
    /// Interaction logic for TrainingCampUX.xaml
    /// </summary>
    public partial class TrainingCampUX : UserControl
    {

        private static ILog logger = LogManager.GetLogger("RollingFile");

        //bindings for UI

        // pw is the parent window mainwindow
        private MainWindow pw;

        public event EventHandler Show_Standings;
        public event EventHandler Set_TopMenu;

        ObservableCollection<TrainingCampStatus> TrainingCamp_Status_list = null;

        public TrainingCampUX(MainWindow pw)
        {
            InitializeComponent();

            this.pw = pw;

            lblTrainingCampHeader.Content = pw.Loaded_League.season.Year.ToString() + " " +
            pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name + " Training Camp";

            TrainingCamp_Services tcs = new TrainingCamp_Services();
            TrainingCamp_Status_list = new ObservableCollection<TrainingCampStatus>(tcs.getTrainingCampStatuses(pw.Loaded_League));

            lstTrainingCamp.ItemsSource = TrainingCamp_Status_list;

            if (pw.Loaded_League.LState != League_State.FreeAgency_Completed &&
                pw.Loaded_League.LState != League_State.Training_Camp_Started)
                btnTrainingCamp.IsEnabled = false;

            if (pw.Loaded_League.LState == League_State.FreeAgency_Completed ||
                pw.Loaded_League.LState == League_State.Training_Camp_Started)
                btnTrainingCamp.IsEnabled = true;
            else
                btnTrainingCamp.IsEnabled = false;
        }
        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }
        private void help_btn_Click(object sender, RoutedEventArgs e)
        {
            var help_form = new Help_TrainingCamp();
            help_form.Top = (SystemParameters.PrimaryScreenHeight - help_form.Height) / 2;
            help_form.Left = (SystemParameters.PrimaryScreenWidth - help_form.Width) / 2;
            help_form.ShowDialog();
        }
        private void lstTrainingCamp_Click(object sender, RoutedEventArgs e)
        {
            ListView ls = (ListView)sender;

             if (ls.SelectedItems.Count > 0)
             {
                try
                {
                    if (Mouse.OverrideCursor == Cursors.Wait) return;

                    TrainingCampStatus tc_Status = TrainingCamp_Status_list[ls.SelectedIndex];

                    if (tc_Status.Status == 3)
                    {
                        BitmapImage HelmetImage = tc_Status.HelmetImage;
                        string TeamName = tc_Status.Team_Name;
                        long year = pw.Loaded_League.season.Year;

                        TrainingCamp_Services tcs = new TrainingCamp_Services();
                        TrainingCampResults tcResult = tcs.getPlayersTrainingCampResult(tc_Status.Franchise_ID, tc_Status.Season_ID, pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name);
                        TrainingCamp_Results_Popup dpp = new TrainingCamp_Results_Popup(HelmetImage, TeamName, year, tcResult);
                        dpp.Left = (SystemParameters.PrimaryScreenWidth - dpp.Width) / 2;
                        dpp.Top = 100;
                        dpp.ShowDialog();
                    }
                    else
                        MessageBox.Show("This team has not completed training camp yet", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    btnTrainingCamp.IsEnabled = true;
                    logger.Error("Error Showing Training Camp Results for Team");
                    logger.Error(ex);
                    MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
 
        }

        private void btnTrainingCamp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                btnTrainingCamp.IsEnabled = false;
                logger.Info("Starting Training Camp");
                foreach (TrainingCampStatus t in TrainingCamp_Status_list)
                if (t.Status != 3) t.Status = 2;

                updateUI(0);
               
                List<long> InProgress_Franchises_List = TrainingCamp_Status_list.Where(x => x.Status != 3).Select(x => x.Franchise_ID).ToList();
                while (InProgress_Franchises_List.Count() > 0)
                {
                    int rnd = CommonUtils.getRandomNum(1, InProgress_Franchises_List.Count()) - 1;
                    long f_id = InProgress_Franchises_List[rnd];
                    TrainingCampStatus tcs = TrainingCamp_Status_list.Where(x => x.Franchise_ID == f_id).First();
                    int tcs_index = TrainingCamp_Status_list.IndexOf(tcs);

                    TrainingCamp_Services tc_service = new TrainingCamp_Services();
                    tc_service.Execute_Team_TrainingCamp(f_id,
                        pw.Loaded_League.season.ID, pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name);
                    TrainingCamp_Status_list[tcs_index].Status = 3;
                    InProgress_Franchises_List.RemoveAt(rnd);
                    updateUI(tcs_index);
                }

                pw.Loaded_League.LState = League_State.Training_Camp_Ended;
                Set_TopMenu?.Invoke(this, new EventArgs());
                logger.Info("Ending executing free agency at beginning of season.");
                Mouse.OverrideCursor = null;
                MessageBox.Show("All Training Camps have Completed.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                btnTrainingCamp.IsEnabled = true;
                logger.Error("Error Conducting Training Camp");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void updateUI(int iIndex)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                new ThreadStart(delegate {
                    lstTrainingCamp.Items.Refresh();
                    lstTrainingCamp.SelectedItem = TrainingCamp_Status_list[iIndex];
                    lstTrainingCamp.ScrollIntoView(TrainingCamp_Status_list[iIndex]);
                }));

        }
    }
}
