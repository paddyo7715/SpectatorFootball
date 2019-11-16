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

namespace SpectatorFootball
{
    public partial class MainWindow
    {
        public Leaguemdl League = null;
        public List<TeamMdl> st_list = null;

        private MainMenuUC MainMenuUC = null;

        private NewLeagueUC NewLeagueUC = null;
        private NewTeamUC NewTeamUC = null;
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
            NewTeamUC = null;
        }
        private void Show_NewLeague(object sender, EventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Entering Create new league");
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
            NewTeamUC = new NewTeamUC(League.Teams[team_ind], "New_League");
            NewTeamUC.setBaseUniform();
            NewTeamUC.setfields();
            NewTeamUC.backtoNewLeague += Back_NewLeague;

            sp_uc.Children.Clear();
            sp_uc.Children.Add(NewTeamUC);

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
                League = null;
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
            logger.Info("Show new Stock Team Form");
            var stock_team = new TeamMdl(0, "");
            NewTeamUC = new NewTeamUC(stock_team, "New_Stock_Team");
            NewTeamUC.setBaseUniform();
            NewTeamUC.setfields();
            NewTeamUC.backtoStockTeams += Show_StockTeams;

            sp_uc.Children.Clear();
            sp_uc.Children.Add(NewTeamUC);
        }
        private void Show_UpdateStockTeam(object sender, StockteamEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                logger.Info("Show Update Stock Team form");

                int team_ind = e.team_ind;
                NewTeamUC = new NewTeamUC(st_list[team_ind], "Update_Stock_Team");
                NewTeamUC.setBaseUniform();
                NewTeamUC.setfields();
                NewTeamUC.backtoStockTeams += Show_StockTeams;

                sp_uc.Children.Clear();
                sp_uc.Children.Add(NewTeamUC);
                Mouse.OverrideCursor = null;
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
