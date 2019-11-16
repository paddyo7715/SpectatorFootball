using System.Windows;
using System.Windows.Input;
using System;
using Microsoft.Win32;
using System.Windows.Controls;

namespace SpectatorFootball
{
    public partial class PlayerNamesUC : UserControl
    {
        public event EventHandler Show_MainMenu;

        public PlayerNamesUC()
        {
            InitializeComponent();
        }

        private void admUbtnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            var OpenFileDialog = new OpenFileDialog();
            if (OpenFileDialog.ShowDialog() == true)
                admtxtSelectFile.Text = OpenFileDialog.FileName;
        }

        private void admBack_Click(object sender, RoutedEventArgs e)
        {
            Show_MainMenu?.Invoke(this, new EventArgs());
        }
        public void clearpage()
        {
            var adm_service = new Administration_Services();
            long[] r;

            try
            {
                admFirstName.Text = "";
                admLastName.Text = "";
                admtxtSelectFile.Text = "";
                r = adm_service.getPlayerNameTotals();
                admtotFirstName.Content = String.Format(r[0].ToString(), "###,###,###,##0");
                admtotLastName.Content = String.Format(r[1].ToString(), "###,###,###,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while retrieving player names. " + CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void admSubmit_Click(object sender, RoutedEventArgs e)
        {
            var adm_service = new Administration_Services();
            long r;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                r = adm_service.AddPlayerNames(admFirstName.Text, admLastName.Text, admtxtSelectFile.Text);
                Mouse.OverrideCursor = null;
                MessageBox.Show(String.Format(r.ToString(), "###,###,###,##0") + " Player Names Added.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                clearpage();
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("An error occured while trying to add the new names " + CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
