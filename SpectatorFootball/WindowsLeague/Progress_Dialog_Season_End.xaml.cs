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
    /// Interaction logic for Progress_Dialog_Season_End.xaml
    /// </summary>
    public partial class Progress_Dialog_Season_End : Window
    {
        public event EventHandler SeasonEnded;
        public Progress_Dialog_Season_End()
        {
            InitializeComponent();
        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            if (statuslbl.Content.ToString() == "Season Ended Successfully!")
            {

 //               leagueCreated?.Invoke(this, new EventArgs());
            }


        }
    }
}

