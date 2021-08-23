using SpectatorFootball.Enum;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.PlayerNS;
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
using System.Windows.Shapes;

namespace SpectatorFootball.WindowsLeague
{
    /// <summary>
    /// Interaction logic for PlayerCard_Popup.xaml
    /// </summary>
    public partial class PlayerCard_Popup : Window
    {
        Player_Card_Data pcd = null;
        public PlayerCard_Popup(Player_Card_Data pcd)
        {
            InitializeComponent();
            string[]  m = Uniform.getTeamDispColors(pcd.team.Home_jersey_Color,
                pcd.team.Home_Jersey_Number_Color,
                pcd.team.Home_Jersey_Number_Outline_Color,
                pcd.team.Helmet_Color,
                pcd.team.Helmet_Logo_Color,
                pcd.team.Home_Pants_Color);

            pcName.Content = (Player_Pos)pcd.Player.Pos + " " + pcd.Player.First_Name + " " + pcd.Player.Last_Name;
            brdCard.BorderBrush = new SolidColorBrush(CommonUtils.getColorfromHex(m[2]));

            lblName.Content = pcName.Content;
            lblTeam.Content = pcd.team.Nickname;

            lblName.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(m[1]));
            lblTeam.Foreground = new SolidColorBrush(CommonUtils.getColorfromHex(m[1]));

            lblName.Background = new SolidColorBrush(CommonUtils.getColorfromHex(m[0]));
            lblTeam.Background = new SolidColorBrush(CommonUtils.getColorfromHex(m[0]));

            imgPlayerFace.Source = new BitmapImage(new Uri(CommonUtils.getAppPath() + "/Images/fplayer.jpg"));

            this.pcd = pcd;

            //If the player is not on a team then do not show the player card tab
            if (pcd.team == null)
            {
                tabPlayer.SelectedIndex = 1;
                tbiCard.Visibility = Visibility.Collapsed;        
            }

            bool bShowPassingStats = false;
            bool bShowRushingStats = false;
            bool bShowRecievingStats = false;
            bool bShowBlockingStats = false;
            bool bShowDefenseStats = false;
            bool bShowPassDefenseStats = false;
            bool bShowFGStats = false;
            bool bShowPuntingStats = false;

            Player_Pos p_pos = ((Player_Pos)pcd.Player.Pos);
            switch (p_pos)
            {
                case Player_Pos.QB:
                    bShowPassingStats = true;
                    bShowRushingStats = true;
                    break;
                case Player_Pos.RB:
                    bShowRushingStats = true;
                    bShowRecievingStats = true;
                    break;
                case Player_Pos.WR:
                    bShowRecievingStats = true;
                    break;
                case Player_Pos.TE:
                    bShowRecievingStats = true;
                    bShowBlockingStats = true;
                    break;
                case Player_Pos.OL:
                    bShowBlockingStats = true;
                    break;
                case Player_Pos.DL:
                    bShowDefenseStats = true;
                    break;
                case Player_Pos.LB:
                    bShowDefenseStats = true;
                    bShowPassDefenseStats = true;
                    break;
                case Player_Pos.DB:
                    bShowPassDefenseStats = true;
                    break;
                case Player_Pos.K:
                    bShowFGStats = true;
                    break;
                case Player_Pos.P:
                    bShowPuntingStats = true;
                    break;
            }

            lstPassing.ItemsSource = pcd.Regular_Season_Stats.Passing_Stats;
            lstRushing.ItemsSource = pcd.Regular_Season_Stats.Rushing_Stats;
            lstReceiving.ItemsSource = pcd.Regular_Season_Stats.Receiving_Stats;
            lstBlocking.ItemsSource = pcd.Regular_Season_Stats.Blocking_Stats;
            lstDefense.ItemsSource = pcd.Regular_Season_Stats.Defense_Stats;
            lstPassDefense.ItemsSource = pcd.Regular_Season_Stats.Pass_Defense_Stats;
            lstFGKicking.ItemsSource = pcd.Regular_Season_Stats.Kicking_Stats;
            lstKickoffReturns.ItemsSource = pcd.Regular_Season_Stats.KickRet_Stats;
            lstPunting.ItemsSource = pcd.Regular_Season_Stats.Punting_Stats;
            lstPuntReturns.ItemsSource = pcd.Regular_Season_Stats.PuntRet_Stats;

            if (pcd.Regular_Season_Stats.Passing_Stats.Count() == 0 && !bShowPassingStats)
            {
                lblPassing.Visibility = Visibility.Collapsed;
                lstPassing.Visibility = Visibility.Collapsed;
                lblPassNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.Rushing_Stats.Count() == 0 && !bShowRushingStats)
            {
                lblRushing.Visibility = Visibility.Collapsed;
                lstRushing.Visibility = Visibility.Collapsed;
                lblRushNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.Receiving_Stats.Count() == 0 && !bShowRecievingStats)
            {
                lblReceiving.Visibility = Visibility.Collapsed;
                lstReceiving.Visibility = Visibility.Collapsed;
                lblReceiveNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.Blocking_Stats.Count() == 0 && !bShowBlockingStats)
            {
                lblBlocking.Visibility = Visibility.Collapsed;
                lstBlocking.Visibility = Visibility.Collapsed;
                lblBlockNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.Defense_Stats.Count() == 0 && !bShowDefenseStats)
            {
                lblDefense.Visibility = Visibility.Collapsed;
                lstDefense.Visibility = Visibility.Collapsed;
                lblDefenseNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.Pass_Defense_Stats.Count() == 0 && !bShowPassDefenseStats)
            {
                lblPassDefense.Visibility = Visibility.Collapsed;
                lstPassDefense.Visibility = Visibility.Collapsed;
                lblPassDefenseNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.Kicking_Stats.Count() == 0 && !bShowFGStats)
            {
                lblFGKicking.Visibility = Visibility.Collapsed;
                lstFGKicking.Visibility = Visibility.Collapsed;
                lblFGKickingNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.KickRet_Stats.Count() == 0)
            {
                lblKickoffReturns.Visibility = Visibility.Collapsed;
                lstKickoffReturns.Visibility = Visibility.Collapsed;
                lblKickReturnsNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.Punting_Stats.Count() == 0 && !bShowPuntingStats)
            {
                lblPunting.Visibility = Visibility.Collapsed;
                lstPunting.Visibility = Visibility.Collapsed;
                lblPuntingNoStats.Visibility = Visibility.Collapsed;
            }

            if (pcd.Regular_Season_Stats.PuntRet_Stats.Count() == 0)
            {
                lblPuntReturns.Visibility = Visibility.Collapsed;
                lstPuntReturns.Visibility = Visibility.Collapsed;
                lblPuntReturnsNoStats.Visibility = Visibility.Collapsed;
            }

            lstPassing_po.ItemsSource = pcd.Playoff_Stats.Passing_Stats;
            lstRushing_po.ItemsSource = pcd.Playoff_Stats.Rushing_Stats;
            lstReceiving_po.ItemsSource = pcd.Playoff_Stats.Receiving_Stats;
            lstBlocking_po.ItemsSource = pcd.Playoff_Stats.Blocking_Stats;
            lstDefense_po.ItemsSource = pcd.Playoff_Stats.Defense_Stats;
            lstPassDefense_po.ItemsSource = pcd.Playoff_Stats.Pass_Defense_Stats;
            lstFGKicking_po.ItemsSource = pcd.Playoff_Stats.Kicking_Stats;
            lstKickoffReturns_po.ItemsSource = pcd.Playoff_Stats.KickRet_Stats;
            lstPunting_po.ItemsSource = pcd.Playoff_Stats.Punting_Stats;
            lstPuntReturns_po.ItemsSource = pcd.Playoff_Stats.PuntRet_Stats;

            if (pcd.Playoff_Stats.Passing_Stats.Count() == 0 && pcd.Playoff_Stats.Rushing_Stats.Count() == 0 &&
                pcd.Playoff_Stats.Receiving_Stats.Count() == 0 && pcd.Playoff_Stats.Blocking_Stats.Count() == 0 &&
                pcd.Playoff_Stats.Defense_Stats.Count() == 0 && pcd.Playoff_Stats.Pass_Defense_Stats.Count() == 0 &&
                pcd.Playoff_Stats.Kicking_Stats.Count() == 0 && pcd.Playoff_Stats.KickRet_Stats.Count() == 0 &&
                pcd.Playoff_Stats.Punting_Stats.Count() == 0 && pcd.Playoff_Stats.PuntRet_Stats.Count() == 0)
                grpPlayoffs.Visibility = Visibility.Collapsed;
            else
            {
                if (pcd.Playoff_Stats.Passing_Stats.Count() == 0 && !bShowPassingStats)
                {
                    lblPassing_po.Visibility = Visibility.Collapsed;
                    lstPassing_po.Visibility = Visibility.Collapsed;
                    lblPassNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.Rushing_Stats.Count() == 0 && !bShowRushingStats)
                {
                    lblRushing_po.Visibility = Visibility.Collapsed;
                    lstRushing_po.Visibility = Visibility.Collapsed;
                    lblRushNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.Receiving_Stats.Count() == 0 && !bShowRecievingStats)
                {
                    lblReceiving_po.Visibility = Visibility.Collapsed;
                    lstReceiving_po.Visibility = Visibility.Collapsed;
                    lblReceiveNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.Blocking_Stats.Count() == 0 && !bShowBlockingStats)
                {
                    lblBlocking_po.Visibility = Visibility.Collapsed;
                    lstBlocking_po.Visibility = Visibility.Collapsed;
                    lblBlockNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.Defense_Stats.Count() == 0 && !bShowDefenseStats)
                {
                    lblDefense_po.Visibility = Visibility.Collapsed;
                    lstDefense_po.Visibility = Visibility.Collapsed;
                    lblDefenseNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.Pass_Defense_Stats.Count() == 0 && !bShowPassDefenseStats)
                {
                    lblPassDefense_po.Visibility = Visibility.Collapsed;
                    lstPassDefense_po.Visibility = Visibility.Collapsed;
                    lblPassDefenseNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.Kicking_Stats.Count() == 0 && !bShowFGStats)
                {
                    lblFGKicking_po.Visibility = Visibility.Collapsed;
                    lstFGKicking_po.Visibility = Visibility.Collapsed;
                    lblFGKickingNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.KickRet_Stats.Count() == 0)
                {
                    lblKickoffReturns_po.Visibility = Visibility.Collapsed;
                    lstKickoffReturns_po.Visibility = Visibility.Collapsed;
                    lblKickReturnsNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.Punting_Stats.Count() == 0 && !bShowPuntingStats)
                {
                    lblPunting_po.Visibility = Visibility.Collapsed;
                    lstPunting_po.Visibility = Visibility.Collapsed;
                    lblPuntingNoStats_po.Visibility = Visibility.Collapsed;
                }

                if (pcd.Playoff_Stats.PuntRet_Stats.Count() == 0)
                {
                    lblPuntReturns_po.Visibility = Visibility.Collapsed;
                    lstPuntReturns_po.Visibility = Visibility.Collapsed;
                    lblPuntReturnsNoStats_po.Visibility = Visibility.Collapsed;
                }
            }

            this.DataContext = this.pcd;
        }
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
