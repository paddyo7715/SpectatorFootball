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
    /// Interaction logic for StatsUX.xaml
    /// </summary>
    public partial class StatsUX : UserControl
    {
        public event EventHandler Show_Standings;
        public StatsUX(MainWindow pw)
        {
            InitializeComponent();
        }

        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }

        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
