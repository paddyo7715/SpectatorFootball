using SpectatorFootball.Playoffs;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for PlayoffsUX.xaml
    /// </summary>
    public partial class PlayoffsUX : UserControl
    {
        private MainWindow pw;
        private List<int> WeeklyGameSlots = null;
        private long num_playoffteams;
        private const double GAMESLOT_LENGTH = 170;
        private const double GAMESLOT_HEIGHT = 60;
        private const double HORIZONTAL_ADJ = 30.0;
        private const double VERTICAL_ADJ = 30;
        private double Canvas_width;
        private double Canvas_height;

        public event EventHandler Show_Standings;
        public PlayoffsUX(MainWindow pw)
        {
            InitializeComponent();
            lblPlayoffHeader.Content = pw.Loaded_League.season.Year + " " +
                pw.Loaded_League.season.League_Structure_by_Season[0].Short_Name +
                " Playoffs";
            this.pw = pw;

            Canvas_width = cnvPlayoffBrackets.Width;


            num_playoffteams = pw.Loaded_League.season.League_Structure_by_Season[0].Num_Playoff_Teams;

            WeeklyGameSlots = Playoff_Helper.PlayoffGamesPerWeek(num_playoffteams);

            setGameSlots();
        }

        private void setGameSlots()
        {
            int weeks = WeeklyGameSlots.Count;
            double h_value = 0.0;
            long gslot_id = 1;

            foreach (int i in WeeklyGameSlots)
            {
                double gs_Left;
                int num_games = i;
                double week_game_height = (GAMESLOT_HEIGHT * num_games) + ((VERTICAL_ADJ) * (num_games-1)) ;
                double gs_running_top = (Canvas_height - week_game_height) / 2;

                double temp = (GAMESLOT_LENGTH * weeks) - HORIZONTAL_ADJ;
                gs_Left = (Canvas_width - temp) / 2;

                for (int j = 1; j < num_games; j++)
                {

                    string gs_name = gs_name = "gw" + i.ToString() + "_" + j.ToString();

                    StackPanel sp = new StackPanel();
                    sp.Name = "gsSlot_" + gslot_id.ToString();
                    sp.Background = Brushes.White;
                    sp.Height = GAMESLOT_HEIGHT;
                    sp.Width = GAMESLOT_LENGTH;

                    cnvPlayoffBrackets.Children.Add(sp);
                    Canvas.SetTop(sp,gs_running_top);
                    Canvas.SetLeft(sp, gs_Left);

                    gslot_id++;
                    gs_running_top += GAMESLOT_HEIGHT + VERTICAL_ADJ;
                }

                gs_Left += GAMESLOT_LENGTH + HORIZONTAL_ADJ;
            }
        }
        private void help_btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnStandings_Click(object sender, RoutedEventArgs e)
        {
            if (Mouse.OverrideCursor == Cursors.Wait) return;
            Show_Standings?.Invoke(this, new EventArgs());
        }
    }


}
