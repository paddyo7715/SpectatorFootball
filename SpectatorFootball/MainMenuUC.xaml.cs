using System.Windows;
using System;
using System.Windows.Controls;
using System.ComponentModel;

namespace SpectatorFootball
{
    public partial class MainMenuUC : UserControl
    {
        public event EventHandler Show_NewLeague;
        public event EventHandler Show_LoadLeague;
        public event EventHandler Show_Options;
        public MainMenuUC()
        {
            InitializeComponent();
        }

        private void mmNew_Click(object sender, RoutedEventArgs e)
        {
            Show_NewLeague?.Invoke(this, new EventArgs());
        }
        private void mmExit_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }
        private void mmOptions_Click(object sender, RoutedEventArgs e)
        {
            Show_Options?.Invoke(this, new EventArgs());
        }

        private void mmLoad_Click(object sender, RoutedEventArgs e)
        {
            Show_LoadLeague?.Invoke(this, new EventArgs());
        }

    }
}
