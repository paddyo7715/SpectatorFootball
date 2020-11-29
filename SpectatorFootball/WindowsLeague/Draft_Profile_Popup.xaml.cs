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
    /// Interaction logic for Draft_Profile.xaml
    /// </summary>
    public partial class Draft_Profile_Popup : Window
    {
        Player binding_player = null;
        public Draft_Profile_Popup(Player p)
        {
            InitializeComponent();
            binding_player = p;
            this.DataContext = binding_player;
        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
