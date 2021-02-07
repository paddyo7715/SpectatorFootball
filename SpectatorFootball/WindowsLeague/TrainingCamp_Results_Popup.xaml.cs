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
using System.Windows.Shapes;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for TrainingCamp_Results_Popup.xaml
    /// </summary>
    public partial class TrainingCamp_Results_Popup : Window
    {
        TrainingCampResults tcResult = null;
        public TrainingCamp_Results_Popup(TrainingCampResults tcResult)
        {
            InitializeComponent();
            this.tcResult = tcResult;

            lvMadeOffense.ItemsSource = tcResult.OffMade;
            lvCutOffense.ItemsSource = tcResult.DefMade;
            lvMadeOffense.ItemsSource = tcResult.OffCut;
            lvCutDefense.ItemsSource = tcResult.DefCut;


        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
