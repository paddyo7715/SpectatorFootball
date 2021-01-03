using log4net;
using SpectatorFootball.Free_Agency;
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
            List<long> fa_order = Team_Helper.getAllFranchiseIDThisSeason(pw.Loaded_League);
            fa_order = CommonUtils.ShufleList(fa_order);
            FreeAgency_Services fas = new FreeAgency_Services();
            while (fa_order.Count() > 0)
            {

                for (int i=0; i < fa_order.Count; i++)
                {
                    Player p = fas.Select_Free_Agent(pw.Loaded_League, fa_order[i], FreeAgency_Players_list.ToList(), FreeAgency_Pick_list.ToList());
                    if (p == null)
                    {
                        fa_order.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        private bool isFreeAgencyDone()
        {
            bool r = false;
            FreeAgency_Helper fah = new FreeAgency_Helper();
            r = fah.isFreeAgencyDone(pw.Loaded_League);
            return r;
        }
    }

}
