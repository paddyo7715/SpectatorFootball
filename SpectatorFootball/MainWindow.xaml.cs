using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.ComponentModel;
using log4net;
using System.Windows.Media;
using SpectatorFootball.Models;
using SpectatorFootball.League;
using SpectatorFootball.Common;

namespace SpectatorFootball
{
    public partial class MainWindow
    {
        //Mem_League is used only when creating a new league.
        public New_League_Structure New_Mem_Season = null;

        //Loaded League Structure
        public Loaded_League_Structure Loaded_League = null;

        public List<Stock_Teams> st_list = null;

        private MainMenuUC MainMenuUC = null;

        private Stock_Team_detail Stock_Team_detailUC = null;
        private New_Team_DetailUC New_Team_DetailUC = null;

        private NewLeagueUC NewLeagueUC = null;
        private PlayerNamesUC PlayerNamesUC = null;
        private StockTeamsUC Stock_teamsUC = null;

        private static ILog logger = LogManager.GetLogger("RollingFile");

        public MainWindow()
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            MainMenuUC = new MainMenuUC();

            sp_uc.Children.Add(MainMenuUC);

            // If run from Visual Studio using the debugger then make the admin menu item visible
           if (!Debugger.IsAttached)
            {
                MenuItem AdminMenuItem;
                AdminMenuItem = (MenuItem)Main_menu_top.Items[Main_menu_top.Items.Count - 1];
                AdminMenuItem.Visibility = Visibility.Collapsed;
            } 

            MainMenuUC.Show_NewLeague += Show_NewLeague;
            MainMenuUC.Show_LoadLeague += Show_LoadLeague;

            logger.Info("Main form created");

        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to exit?", "Exiting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.No)
                e.Cancel = true;
            else
            {
                logger.Info("User closing application");
                CloseApplication();
            }
        }
        private void mmTopExit_Click(object sender, RoutedEventArgs e)  
        {
            this.Close();
        }

        private void CloseApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Show_MainMenu(object sender, EventArgs e)
        {
            logger.Info("Showing Main Menu");
            sp_uc.Children.Clear();
            sp_uc.Children.Add(MainMenuUC);
            NewLeagueUC = null;
            New_Team_DetailUC = null;
            Stock_Team_detailUC = null;
            New_Team_DetailUC = null;
            PlayerNamesUC = null;
            Stock_teamsUC = null;
        }
        private void Show_NewLeague(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;

                logger.Info("Entering Create new league");
                New_Mem_Season = new New_League_Structure();
                New_Mem_Season.Season = new Season();
                New_Mem_Season.Season.League_Structure_by_Season.Add(new League_Structure_by_Season());

                StockTeams_Services sts = new StockTeams_Services();
                st_list = sts.getAllStockTeams();
                logger.Debug("Stock Teams Loaded");
                NewLeagueUC = new NewLeagueUC(this, st_list);

                NewLeagueUC.Show_MainMenu += Show_MainMenu;
                NewLeagueUC.Show_NewTeam += this.Show_NewTeamDetail;

                sp_uc.Children.Clear();
                sp_uc.Children.Add(NewLeagueUC);
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error showing new form");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Back_NewLeague(object sender, TeamUpdatedEventArgs e)
        {

            // Only if the team is updated, update the team labels
            if (e.team_upd == true)
                NewLeagueUC.setTeamsLabels();

            sp_uc.Children.Clear();
            sp_uc.Children.Add(NewLeagueUC);
        }
        private void Show_NewTeamDetail(object sender, teamEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            logger.Info("Show new team detail");

            int team_ind = e.team_num - 1;

            New_Team_DetailUC = new New_Team_DetailUC(New_Mem_Season.Season.Teams_by_Season[team_ind], true);
            New_Team_DetailUC.backtoNewLeague += Back_NewLeague;

            sp_uc.Children.Clear();
            sp_uc.Children.Add(New_Team_DetailUC);

            Mouse.OverrideCursor = null;
        }
        private void Show_PlayerNames(object sender, EventArgs e)
        {
            logger.Info("Show Player names");

            PlayerNamesUC = new PlayerNamesUC();
            PlayerNamesUC.clearpage();
            PlayerNamesUC.Show_MainMenu += Show_MainMenu;

            sp_uc.Children.Clear();
            sp_uc.Children.Add(PlayerNamesUC);
        }
        private void Show_StockTeams(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Show stock teams");
                var sts = new StockTeams_Services();
                st_list = sts.getAllStockTeams();

                logger.Debug("Stock Team List retrieved");

                Stock_teamsUC = new StockTeamsUC(st_list);
                Stock_teamsUC.Show_MainMenu += Show_MainMenu;
                Stock_teamsUC.Show_NewStockTeam += Show_NewStockTeam;
                Stock_teamsUC.Show_UpdateStockTeam += this.Show_UpdateStockTeam;
                sp_uc.Children.Clear();
                sp_uc.Children.Add(Stock_teamsUC);
                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Showing Stock Team Management Form");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Show_NewStockTeam(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Show new Stock Team Form");
                Stock_Team_detailUC = new Stock_Team_detail(null);
                Stock_Team_detailUC.backtoStockTeams += Show_StockTeams;

                sp_uc.Children.Clear();
                sp_uc.Children.Add(Stock_Team_detailUC);
//                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Showing New Stock Team Detail Form");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

//This event handler is in response to the user clicking the load league button from the main menu
//and displays the load league dialog box.
        private void Show_LoadLeague(object sender, EventArgs e)
        {

            Loaded_League = null;
            var LL_form = new LoadLeague();
            LL_form.Load_League += Load_League;
            LL_form.Top = (SystemParameters.PrimaryScreenHeight - LL_form.Height) / 2;
            LL_form.Left = (SystemParameters.PrimaryScreenWidth - LL_form.Width) / 2;
            LL_form.ShowDialog();

        }

        private void Load_League(object sender, LoadLeagueEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Attempting to Load League " + e.League_Short_Name);

                Loaded_League = new Loaded_League_Structure();

                League_Services ls = new League_Services();
                string[] r = ls.CheckDBVersion((string)e.League_Short_Name);

                int r_code = int.Parse(r[0]);
                if (r_code == 2) //can not load league
                    MessageBox.Show("Can not load league because the database and program versions are incompatible!","Error!",MessageBoxButton.OK,MessageBoxImage.Error);
                else if (r_code == 1)
                {
                    var response = MessageBox.Show("This league and your program have different versions.  Do you wish to upgrade your league file to match the program?  Note tha your league file will be backed up before an update is attempted,  League database file vesion " + r[1] + " program vesion " + r[2], "Update??", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (response == MessageBoxResult.Yes)
                    {
                        logger.Info("User decided to upgrade database.");
                        ls.UpgradeDB((string)e.League_Short_Name, r[1], r[2]);
                    }
                    else
                    {                        
                        logger.Info("User decided not to upgrade database.");
                    }
                }

                //Load the league season.  Null for year parameter mean load the latest year
                Loaded_League.season = ls.LoadSeason(null, (string)e.League_Short_Name);
                Loaded_League.Current_Year = Loaded_League.season.Year;

                //Set league state
                Loaded_League.LState = ls.getSeasonState("", Loaded_League.season.ID, (string)e.League_Short_Name);

                //Load the league standings
                Loaded_League.Standings = ls.getLeageStandings(Loaded_League.season.ID, (string)e.League_Short_Name);

                //if league has been loaded then show the league standings window.

                Mouse.OverrideCursor = null;
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Loading League " + e.League_Short_Name);
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Show_UpdateStockTeam(object sender, StockteamEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Show Update Stock Team form");

                int team_ind = e.team_ind;
                Stock_Team_detailUC = new Stock_Team_detail(st_list[team_ind]);
                Stock_Team_detailUC.backtoStockTeams += Show_StockTeams;

                sp_uc.Children.Clear();
                sp_uc.Children.Add(Stock_Team_detailUC);
 
            }
            catch (Exception ex)
            {
                Mouse.OverrideCursor = null;
                logger.Error("Error Showing Update Stock Team Form");
                logger.Error(ex);
                MessageBox.Show(CommonUtils.substr(ex.Message, 0, 100), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
