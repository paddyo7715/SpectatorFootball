using System;
using System.Drawing;
using System.Windows;

namespace SpectatorFootball
{

    public partial class Progress_Dialog
    {

        public event EventHandler leagueCreated;
        public Progress_Dialog()
        {

            // This call is required by the designer.
            InitializeComponent();
        }

            private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            if (statuslbl.Content.ToString() == "League Created Successfully!")
            {
                MessageBox.Show("Your League has been Created!  \nYou will now be Brought Back to the Main Menue, where you can Load Your League.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                leagueCreated?.Invoke(this, new EventArgs());
            }


        }
    }
}
