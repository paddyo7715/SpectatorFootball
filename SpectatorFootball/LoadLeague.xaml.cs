using SpectatorFootball.Common;
using System;
using System.Collections.Generic;
using System.IO;
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
//using System.Windows.Shapes;

namespace SpectatorFootball
{
    /// <summary>
    /// Interaction logic for LoadLeague.xaml
    /// </summary>
    public partial class LoadLeague : Window
    {

        public event EventHandler<LoadLeagueEventArgs> Load_League;

        public LoadLeague()
        {
            InitializeComponent();
            //Load all leagues
            string game_data_folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER;
            string[] sf_data_folders = Directory.GetDirectories(game_data_folder);
            bool bLeaguesFound = false;

            foreach (string d in sf_data_folders)
            {
                string league_name = d.Substring(d.LastIndexOf(Path.DirectorySeparatorChar)+1);

                if (league_name == app_Constants.LOG_FOLDER)
                    continue;

                if (File.Exists(d + Path.DirectorySeparatorChar + league_name + "." + app_Constants.DB_FILE_EXT))
                {
                    string[] s = getLeagueSettings(d);

                    var h_sp = new StackPanel();
                    h_sp.Orientation = Orientation.Horizontal;

                    BitmapImage BitmapImage = null;
                    Image logo_img = null;

                    if (s[0].Length > 0)
                    {
                        BitmapImage = new BitmapImage(new Uri(d + Path.DirectorySeparatorChar + s[0]));
                        logo_img = new Image();
                        logo_img.Width = 50;
                        logo_img.Height = 40;
                        logo_img.Source = BitmapImage;
                        h_sp.Children.Add(logo_img);
                    }
                    else
                    {
                        Label lblShortName = new Label();
                        lblShortName.Content = league_name;
                        h_sp.Children.Add(lblShortName);
                    }

                    var team_label = new Label();
                    team_label.Content = s[1] + " " + "(" + league_name + ")";
                    team_label.Height = 25;
                    team_label.Width = 280;
                    team_label.VerticalContentAlignment = VerticalAlignment.Center;
                    team_label.HorizontalContentAlignment = HorizontalAlignment.Left;

                    team_label.FontFamily = new FontFamily("Times New Roman");
                    team_label.FontSize = 12;

                    h_sp.Children.Add(team_label);
                    // h_sp.Children.Add(std_img)
                    h_sp.Margin = new Thickness(1);

                    LoadLeagueGrid.Items.Add(h_sp);
                    bLeaguesFound = true;
                }
            }
            //If no loeages found to load then show the user and prevent selections
            if (!bLeaguesFound)
            {
                LoadLeagueGrid.Items.Add("No Leagues to Load.");
                LoadLeagueGrid.Focusable = false;
                LoadLeagueGrid.IsEnabled = false;
                LoadLeage_load.IsEnabled = false;
            }
            else
            {
                LoadLeagueGrid.Focusable = true;
                LoadLeagueGrid.IsEnabled = true;
                LoadLeage_load.IsEnabled = true;
            }

        }

        private string[] getLeagueSettings(string league_folder)
        {
            string League_Logo_filepath = "";
            string League_Long_Name = "";

            if (File.Exists(league_folder + Path.DirectorySeparatorChar + app_Constants.LEAGUE_PROFILE_FILE))
            {
                foreach (string line in File.ReadAllLines(league_folder + Path.DirectorySeparatorChar + app_Constants.LEAGUE_PROFILE_FILE))
                {
                    if (line.StartsWith("LongName:"))
                        League_Long_Name = line.Split(':')[1];
                    else if (line.StartsWith("LogoFileName:"))
                        League_Logo_filepath = line.Split(':')[1];
                }
            }

            return new string[] { League_Logo_filepath.Trim(), League_Long_Name.Trim() };
        }
            


        private void LoadLeage_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadLeage_load_Click(object sender, RoutedEventArgs e)
        {
            if (LoadLeagueGrid.SelectedIndex == -1)
                MessageBox.Show("Please Select a League to Load.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                StackPanel league_row = (StackPanel)LoadLeagueGrid.SelectedItem;
                string League_Short_Name = null;
                string league_string_content = null;
                Label League_Name_Lable = null;

                if (league_row.Children.Count == 2)
                    League_Name_Lable = (Label)league_row.Children[1];
                else
                    League_Name_Lable = (Label)league_row.Children[0];

                league_string_content = (string)League_Name_Lable.Content;

                int s_pos = league_string_content.ToString().IndexOf("(");
                int e_pos = league_string_content.ToString().IndexOf(")");

                if ((s_pos == 01) || (e_pos == -1) || (s_pos >= e_pos))
                    MessageBox.Show("Error in Format of League Row.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    League_Short_Name = league_string_content.ToString().Substring(s_pos+1, e_pos - s_pos - 1);
                    Load_League?.Invoke(this, new LoadLeagueEventArgs(League_Short_Name));
                    this.Close();
                }

            }
        }
    }
}
