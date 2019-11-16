using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Controls;
using System;
using System.Data;
using System.IO;
using log4net;

namespace SpectatorFootball
{
    public partial class StockTeamsUC : UserControl
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        private List<TeamMdl> st_list = null;

        public event EventHandler Show_MainMenu;
        public event EventHandler Show_NewStockTeam;
        public event EventHandler<StockteamEventArgs> Show_UpdateStockTeam;

        public StockTeamsUC(List<TeamMdl> st_list)
        {

            // This call is required by the designer.
            InitializeComponent();

            this.st_list = st_list;

            setStockTeams();
        }
        private void setStockTeams()
        {
            StockTeamsGrid.Items.Clear();

            foreach (var st in st_list)
            {
                var h_sp = new StackPanel();
                h_sp.Orientation = Orientation.Horizontal;

                var BitmapImage = new BitmapImage(new Uri(CommonUtils.getAppPath() + Convert.ToString(Path.DirectorySeparatorChar) + "Images" + Path.DirectorySeparatorChar + "Helmets" + Path.DirectorySeparatorChar + st.Helmet_img_path));
                var helmet_img = new Image();
                helmet_img.Width = 50;
                helmet_img.Height = 50;
                helmet_img.Source = BitmapImage;

                List<Uniform_Color_percents> Color_Percents_List = null;
                Color_Percents_List = Uniform.getColorList(st.Uniform);

                var team_label = new Label();
                team_label.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List[0].color_string));
                team_label.Content = st.City + " " + st.Nickname;
                team_label.Height = 50;
                team_label.Width = 360;
                team_label.VerticalContentAlignment = VerticalAlignment.Center;

                if (Color_Percents_List.Count > 2)
                {
                    var BackBrush = new LinearGradientBrush();
                    BackBrush.StartPoint = new Point(0, 0);
                    BackBrush.EndPoint = new Point(1, 1);

                    float running_value = 0;
                    for (int i = 1; i <= Color_Percents_List.Count - 1; i++)
                    {
                        BackBrush.GradientStops.Add(new GradientStop(CommonUtils.getColorfromHex(Color_Percents_List[i].color_string), running_value));

                        running_value = Color_Percents_List[i].value;

                        BackBrush.GradientStops.Add(new GradientStop(CommonUtils.getColorfromHex(Color_Percents_List[i].color_string), running_value));
                    }
                    team_label.Background = BackBrush;
                }
                else
                    team_label.Background = new SolidColorBrush(CommonUtils.getColorfromHex(Color_Percents_List[1].color_string));
                team_label.FontFamily = new FontFamily("Times New Roman");
                team_label.FontSize = 24;


                var BitmapImageST = new BitmapImage(new Uri(CommonUtils.getAppPath() + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + "Stadiums" + Path.DirectorySeparatorChar + st.Stadium.Stadium_Img_Path));
                var std_img = new Image();
                std_img.Width = 80;
                std_img.Height = 50;

                std_img.Source = BitmapImageST;

                h_sp.Children.Add(helmet_img);
                h_sp.Children.Add(team_label);
                h_sp.Children.Add(std_img);
                h_sp.Margin = new Thickness(5);

                StockTeamsGrid.Items.Add(h_sp);
            }
        }
        private void canstockT_Click(object sender, RoutedEventArgs e)
        {
            Show_MainMenu?.Invoke(this, new EventArgs());
        }
        private void EditstockT_Click(object sender, RoutedEventArgs e)
        {
            int i = StockTeamsGrid.SelectedIndex;
            Show_UpdateStockTeam?.Invoke(this, new StockteamEventArgs(i));
        }
        private void AddstockT_Click(object sender, RoutedEventArgs e)
        {
            Show_NewStockTeam?.Invoke(this, new EventArgs());
        }

        private void DelstockT_Click(object sender, RoutedEventArgs e)
        {
            StockTeams_Services sts = null;
            int num_selected = StockTeamsGrid.SelectedIndex;

            int id = st_list[num_selected].id;

            if (id != -1)
            {
                var response = MessageBox.Show("Do you really want to delete ", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if ((int)response == (int)MessageBoxResult.Yes)
                {
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        sts = new StockTeams_Services();
                        sts.DeleteStockTeam(id);
                        var d_team = st_list[num_selected];
                        st_list.Remove(d_team);
                        setStockTeams();
                        Mouse.OverrideCursor = null;
                    }
                    catch (Exception ex)
                    {
                        Mouse.OverrideCursor = null;
                        logger.Error("Error deleting stock team " + ex.Message);
                        logger.Error(ex);
                        MessageBox.Show("Error deleting stock team", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
                MessageBox.Show("You must first select a stock team to delete ", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
