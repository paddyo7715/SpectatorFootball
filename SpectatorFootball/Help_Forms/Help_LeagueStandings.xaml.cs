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

namespace SpectatorFootball.Help_Forms
{
    /// <summary>
    /// Interaction logic for Help_LeagueStandings.xaml
    /// </summary>
    public partial class Help_LeagueStandings : Window
    {
        public Help_LeagueStandings()
        {
            InitializeComponent();
        }

        private void hlp_nl_close_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
