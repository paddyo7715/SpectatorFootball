using System.Windows;
using System;
using System.Windows.Controls;
using System.ComponentModel;

namespace SpectatorFootball
{
    public partial class MainMenuUC : UserControl
    {
        public event EventHandler Show_NewLeague;

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
        private void mmAdmin_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
