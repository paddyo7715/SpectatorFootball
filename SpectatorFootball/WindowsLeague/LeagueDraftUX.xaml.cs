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
    /// Interaction logic for LeagueDraftUX.xaml
    /// </summary>
    public partial class LeagueDraftUX : UserControl
    {
        public event EventHandler Show_Standings;
        public LeagueDraftUX()
        {
            InitializeComponent();
        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stockPick_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPick_Click(object sender, RoutedEventArgs e)
        {

        }

        private void simRound_Click(object sender, RoutedEventArgs e)
        {

        }

        private void simDraft_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
