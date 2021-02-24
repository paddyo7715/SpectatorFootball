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
        public TrainingCamp_Results_Popup(BitmapImage HelmetImage, string TeamName, long Year, TrainingCampResults tcResult)
        {
            InitializeComponent();
            imgTeamHelmet.Source = HelmetImage;
            lblTeamName.Content = TeamName;
            lblTrainingCampYear.Content = Year.ToString() + " Training Camp Results";

            this.tcResult = tcResult;

            lvMadeOffense.ItemsSource = tcResult.OffMade;
            lvMadeDefense.ItemsSource = tcResult.DefMade;
            lvCutOffense.ItemsSource = tcResult.OffCut;
            lvCutDefense.ItemsSource = tcResult.DefCut;

        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
