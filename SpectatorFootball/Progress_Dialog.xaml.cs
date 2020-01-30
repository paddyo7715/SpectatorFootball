using System.Windows;

namespace SpectatorFootball
{

    public partial class Progress_Dialog
    {
        public Progress_Dialog()
        {

            // This call is required by the designer.
            InitializeComponent();
        }

            private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
