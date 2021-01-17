using log4net;
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

        public TrainingCampUX(MainWindow pw)
        {
            InitializeComponent();
            this.pw = pw;
        }
        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }
        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnTrainingCamp_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
