using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
    /// Interaction logic for BoxScore_Popup.xaml
    /// </summary>
    public partial class BoxScore_Popup : Window
    {
        public BoxScore bs_rec { get; set; }
        Style popupTC = (Style)System.Windows.Application.Current.FindResource("popupTC");
        private bool bPenalties;

        public BoxScore_Popup(BoxScore bs_rec, bool bPenalties)
        {
            InitializeComponent();
            this.bs_rec = bs_rec;
            DataContext = this;
            setBoxScoreValues();
        }
        //Need to set box score tab values in code because many of the 
        //values are calculated
        private void setBoxScoreValues()
        {
            //Add team city abbriviations to box score
            var lblBlank = new Label();
            lblBlank.Name = "";
            lblBlank.Width = 150;
            lblBlank.Style = popupTC;

            var lblAwayTeam = new Label();
            lblAwayTeam.Name = bs_rec.AwayCityAbbr;
            lblAwayTeam.Width = 150;
            lblAwayTeam.Style = popupTC;

            var lblHomeTeam = new Label();
            lblHomeTeam.Name = bs_rec.HomeCityAbbr;
            lblHomeTeam.Width = 150;
            lblHomeTeam.Style = popupTC;

            var v_teams = new StackPanel();
            v_teams.Orientation = Orientation.Vertical;
            v_teams.VerticalAlignment = VerticalAlignment.Center;
            v_teams.HorizontalAlignment = HorizontalAlignment.Left;

            v_teams.Children.Add(lblBlank);
            v_teams.Children.Add(lblAwayTeam);
            v_teams.Children.Add(lblHomeTeam);

            spQTRScore.Children.Add(v_teams);
            //Add first qtr score
            var lblQTR1 = new Label();
            lblQTR1.Name = "1";
            lblQTR1.Width = 150;
            lblQTR1.Style = popupTC;

            var lblAwayQTR1 = new Label();
            lblAwayQTR1.Name = bs_rec.Game.Away_Score_Q1.ToString();
            lblAwayQTR1.Width = 150;
            lblAwayQTR1.Style = popupTC;

            var lblHomeQTR1 = new Label();
            lblHomeQTR1.Name = bs_rec.Game.Home_Score_Q1.ToString();
            lblHomeQTR1.Width = 150;
            lblHomeQTR1.Style = popupTC;

            var v_QTR1 = new StackPanel();
            v_QTR1.Orientation = Orientation.Vertical;
            v_QTR1.VerticalAlignment = VerticalAlignment.Center;
            v_QTR1.HorizontalAlignment = HorizontalAlignment.Left;

            v_QTR1.Children.Add(lblQTR1);
            v_QTR1.Children.Add(lblAwayQTR1);
            v_QTR1.Children.Add(lblHomeQTR1);

            spQTRScore.Children.Add(v_QTR1);
            //Add second qtr score
            var lblQTR2 = new Label();
            lblQTR2.Name = "2";
            lblQTR2.Width = 150;
            lblQTR2.Style = popupTC;

            var lblAwayQTR2 = new Label();
            lblAwayQTR2.Name = bs_rec.Game.Away_Score_Q2.ToString();
            lblAwayQTR2.Width = 150;
            lblAwayQTR2.Style = popupTC;

            var lblHomeQTR2 = new Label();
            lblHomeQTR2.Name = bs_rec.Game.Home_Score_Q2.ToString();
            lblHomeQTR2.Width = 150;
            lblHomeQTR2.Style = popupTC;

            var v_QTR2 = new StackPanel();
            v_QTR2.Orientation = Orientation.Vertical;
            v_QTR2.VerticalAlignment = VerticalAlignment.Center;
            v_QTR2.HorizontalAlignment = HorizontalAlignment.Left;

            v_QTR2.Children.Add(lblQTR2);
            v_QTR2.Children.Add(lblAwayQTR2);
            v_QTR2.Children.Add(lblHomeQTR2);

            spQTRScore.Children.Add(v_QTR2);
            //Add third qtr score
            var lblQTR3 = new Label();
            lblQTR3.Name = "3";
            lblQTR3.Width = 150;
            lblQTR3.Style = popupTC;

            var lblAwayQTR3 = new Label();
            lblAwayQTR3.Name = bs_rec.Game.Away_Score_Q3.ToString();
            lblAwayQTR3.Width = 150;
            lblAwayQTR3.Style = popupTC;

            var lblHomeQTR3 = new Label();
            lblHomeQTR3.Name = bs_rec.Game.Home_Score_Q3.ToString();
            lblHomeQTR3.Width = 150;
            lblHomeQTR3.Style = popupTC;

            var v_QTR3 = new StackPanel();
            v_QTR3.Orientation = Orientation.Vertical;
            v_QTR3.VerticalAlignment = VerticalAlignment.Center;
            v_QTR3.HorizontalAlignment = HorizontalAlignment.Left;

            v_QTR3.Children.Add(lblQTR3);
            v_QTR3.Children.Add(lblAwayQTR3);
            v_QTR3.Children.Add(lblHomeQTR3);

            spQTRScore.Children.Add(v_QTR3);
            //Add fourth qtr score
            var lblQTR4 = new Label();
            lblQTR4.Name = "4";
            lblQTR4.Width = 150;
            lblQTR4.Style = popupTC;

            var lblAwayQTR4 = new Label();
            lblAwayQTR4.Name = bs_rec.Game.Away_Score_Q4.ToString();
            lblAwayQTR4.Width = 150;
            lblAwayQTR4.Style = popupTC;

            var lblHomeQTR4 = new Label();
            lblHomeQTR4.Name = bs_rec.Game.Home_Score_Q4.ToString();
            lblHomeQTR4.Width = 150;
            lblHomeQTR4.Style = popupTC;

            var v_QTR4 = new StackPanel();
            v_QTR4.Orientation = Orientation.Vertical;
            v_QTR4.VerticalAlignment = VerticalAlignment.Center;
            v_QTR4.HorizontalAlignment = HorizontalAlignment.Left;

            v_QTR4.Children.Add(lblQTR4);
            v_QTR4.Children.Add(lblAwayQTR4);
            v_QTR4.Children.Add(lblHomeQTR4);

            spQTRScore.Children.Add(v_QTR4);
            //only if the game went into overtime show the ot column
            if (bs_rec.Game.Quarter > 4)
            {
                //Add OT qtr score
                var lblQTROT = new Label();
                lblQTROT.Name = "OT";
                lblQTROT.Width = 150;
                lblQTROT.Style = popupTC;

                var lblAwayQTROT = new Label();
                lblAwayQTROT.Name = bs_rec.Game.Away_Score_OT.ToString();
                lblAwayQTROT.Width = 150;
                lblAwayQTROT.Style = popupTC;

                var lblHomeQTROT = new Label();
                lblHomeQTROT.Name = bs_rec.Game.Home_Score_OT.ToString();
                lblHomeQTROT.Width = 150;
                lblHomeQTROT.Style = popupTC;

                var v_QTROT = new StackPanel();
                v_QTROT.Orientation = Orientation.Vertical;
                v_QTROT.VerticalAlignment = VerticalAlignment.Center;
                v_QTROT.HorizontalAlignment = HorizontalAlignment.Left;

                v_QTROT.Children.Add(lblQTROT);
                v_QTROT.Children.Add(lblAwayQTROT);
                v_QTROT.Children.Add(lblHomeQTROT);

                spQTRScore.Children.Add(v_QTROT);
            }

            //Add Final score
            var lblQTRFINAL = new Label();
            lblQTRFINAL.Name = "OT";
            lblQTRFINAL.Width = 150;
            lblQTRFINAL.Style = popupTC;

            var lblAwayQTRFINAL = new Label();
            lblAwayQTRFINAL.Name = bs_rec.Game.Away_Score.ToString();
            lblAwayQTRFINAL.Width = 150;
            lblAwayQTRFINAL.Style = popupTC;

            var lblHomeQTRFINAL = new Label();
            lblHomeQTRFINAL.Name = bs_rec.Game.Home_Score.ToString();
            lblHomeQTRFINAL.Width = 150;
            lblHomeQTRFINAL.Style = popupTC;

            var v_QTRFINAL = new StackPanel();
            v_QTRFINAL.Orientation = Orientation.Vertical;
            v_QTRFINAL.VerticalAlignment = VerticalAlignment.Center;
            v_QTRFINAL.HorizontalAlignment = HorizontalAlignment.Left;

            v_QTRFINAL.Children.Add(lblQTRFINAL);
            v_QTRFINAL.Children.Add(lblAwayQTRFINAL);
            v_QTRFINAL.Children.Add(lblHomeQTRFINAL);

            spQTRScore.Children.Add(v_QTRFINAL);

            lblBSAwayAbbrv.Content = bs_rec.AwayCityAbbr;
            lblBSHomeAbbrv.Content = bs_rec.HomeCityAbbr;

            lblAwayBSFirstDowns.Content = bs_rec.Game.Away_FirstDowns;
            lblHomeBSFirstDowns.Content = bs_rec.Game.Home_FirstDowns;

            //get total rushing yars
            long awayRushYars = bs_rec.Game.Game_Player_Rushing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Rush_Yards);
            long homeRushYars = bs_rec.Game.Game_Player_Rushing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Rush_Yards);

            lblAwayBSRushingYards.Content = awayRushYars;
            lblHomeBSRushingYards.Content = homeRushYars;

            long awayPassYars = bs_rec.Game.Game_Player_Passing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Pass_Yards);
            long homePassYars = bs_rec.Game.Game_Player_Passing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Pass_Yards);

            lblAwayBSPassingYards.Content = awayPassYars;
            lblHomeBSPassingYards.Content = homePassYars;

            lblAwayBSTotalYards.Content = awayRushYars + awayPassYars;
            lblHomeBSTotalYards.Content = homeRushYars + homePassYars;

            lblAwayBS3rdDownConv.Content = bs_rec.Game.Away_3Point_Conv_Made.ToString() + "-" + bs_rec.Game.Away_3Point_Conv_Att.ToString();
            lblHomeBS3rdDownConv.Content = bs_rec.Game.Home_3Point_Conv_Made.ToString() + "-" + bs_rec.Game.Home_3Point_Conv_Att.ToString();

            lblAwayBS4thDownConv.Content = bs_rec.Game.Away_FourthDown_Conversions.ToString() + "-" + bs_rec.Game.Away_FourthDowns.ToString();
            lblHomeBS4thDownConv.Content = bs_rec.Game.Home_FourthDown_Conversions.ToString() + "-" + bs_rec.Game.Home_FourthDowns.ToString();

            long away_turnovers = 0;
            long home_turnovers = 0;

            //add qb interceptions to turnovers
            away_turnovers += bs_rec.Game.Game_Player_Passing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Pass_Ints);
            home_turnovers += bs_rec.Game.Game_Player_Passing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Pass_Ints);

            //add qb fumbles lost to turnovers
            away_turnovers += bs_rec.Game.Game_Player_Passing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);
            home_turnovers += bs_rec.Game.Game_Player_Passing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);

            //add kickoff returner muffed kickoffs lost
            away_turnovers += bs_rec.Game.Game_Player_Kick_Returner_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Kickoffs_Muffed_Lost);
            home_turnovers += bs_rec.Game.Game_Player_Kick_Returner_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Kickoffs_Muffed_Lost);

            //add kickoff returner fumbles lost
            away_turnovers += bs_rec.Game.Game_Player_Kick_Returner_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Kickoffs_Returned_Fumbles_Lost);
            home_turnovers += bs_rec.Game.Game_Player_Kick_Returner_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Kickoffs_Returned_Fumbles_Lost);

            //add punt returner fumbles lost
            away_turnovers += bs_rec.Game.Game_Player_Punt_Returner_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Punts_Returned_Fumbles_Lost);
            home_turnovers += bs_rec.Game.Game_Player_Punt_Returner_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Punts_Returned_Fumbles_Lost);

            //add punter fumbles lost
            away_turnovers += bs_rec.Game.Game_Player_Punter_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);
            home_turnovers += bs_rec.Game.Game_Player_Punter_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);

            //add receiver fumbles lost
            away_turnovers += bs_rec.Game.Game_Player_Receiving_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);
            home_turnovers += bs_rec.Game.Game_Player_Receiving_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);

            //add rusher fumbles lost
            away_turnovers += bs_rec.Game.Game_Player_Rushing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);
            home_turnovers += bs_rec.Game.Game_Player_Rushing_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Fumbles_Lost);

            lblAwayBSTurnovers.Content = away_turnovers;
            lblHomeBSTurnovers.Content = home_turnovers;

            long awayFGmade = bs_rec.Game.Game_Player_Kicker_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.FG_Made);

            long awayFGAtt = bs_rec.Game.Game_Player_Kicker_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.FG_Att);

            long homeFGmade = bs_rec.Game.Game_Player_Kicker_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.FG_Made);

            long homeFGAtt = bs_rec.Game.Game_Player_Kicker_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.FG_Att);

            lblAwayBSFieldGoals.Content = awayFGmade.ToString() + "-" + awayFGAtt.ToString();
            lblHomeBSFieldGoals.Content = awayFGmade.ToString() + "-" + awayFGAtt.ToString();

            lblAwayBSSacks.Content = bs_rec.Game.Game_Player_Defense_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Sum(x => x.Def_Sacks);
            lblHomeBSSacks.Content = bs_rec.Game.Game_Player_Defense_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Sum(x => x.Def_Sacks);

            lblAwayBSTimeofPoss.Content = CommonUtils.getTimestringFromSeconds((long)bs_rec.Game.Away_TOP);
            lblHomeBSTimeofPoss.Content = CommonUtils.getTimestringFromSeconds((long)bs_rec.Game.Home_TOP);

//Only if the league is configured for penalties then show the penalty count for the game
            if (bPenalties)
            {
                lblAwayPenalties.Content = lblAwayBSSacks.Content = bs_rec.Game.Game_Player_Penalty_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID).Count();
                lblAwayPenalties.Content = lblHomeBSSacks.Content = bs_rec.Game.Game_Player_Penalty_Stats.Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID).Count();
                lblPenalties.Visibility = Visibility.Visible;
                lblAwayPenalties.Visibility = Visibility.Visible;
                lblAwayPenalties.Visibility = Visibility.Visible;
            }

            //because ef6 is limited and can not sort or filter subentries (includes), I must do it myself.
            //For example, I do not want to include QBs that don't have any attempts in the stats line
            List<Game_Player_Passing_Stats> away_Passing_stats = bs_rec.Game.Game_Player_Passing_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID && x.Pass_Att > 0)
                .OrderByDescending(x => x.Pass_Yards).ToList();

            List<Game_Player_Rushing_Stats> away_Rushing_stats = bs_rec.Game.Game_Player_Rushing_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID && x.Rush_Att > 0)
                .OrderByDescending(x => x.Rush_Yards).ToList();

            List<Game_Player_Receiving_Stats> away_Receiving_stats = bs_rec.Game.Game_Player_Receiving_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID && x.Rec_Catches > 0)
                .OrderByDescending(x => x.Rec_Yards).ToList();

            List<Game_Player_Pass_Defense_Stats> away_Pass_Defense_stats = bs_rec.Game.Game_Player_Pass_Defense_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID && (x.Ints > 0 || x.Def_Pass_Defenses > 0))
                .OrderByDescending(x => x.Ints).ThenByDescending(x => x.Def_Pass_Defenses).ToList();

            List<Game_Player_Defense_Stats> away_Defense_stats = bs_rec.Game.Game_Player_Defense_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID && x.Def_Tackles > 0)
                .OrderByDescending(x => x.Def_Tackles).ToList();

            List<Game_Player_Kicker_Stats> away_kicker_stats = bs_rec.Game.Game_Player_Kicker_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID)
                .OrderByDescending(x => x.FG_Made).ToList();

            List<Game_Player_Punter_Stats> away_punter_stats = bs_rec.Game.Game_Player_Punter_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID)
                .OrderByDescending(x => x.num_punts).ToList();

            List<Game_Player_Kick_Returner_Stats> away_KickoffReturns_stats = bs_rec.Game.Game_Player_Kick_Returner_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID)
                .OrderByDescending(x => x.Kickoffs_Returned).ToList();

            List<Game_Player_Punt_Returner_Stats> away_PuntReturns_stats = bs_rec.Game.Game_Player_Punt_Returner_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Away_Team_Franchise_ID)
                .OrderByDescending(x => x.Punts_Returned).ToList();


            List<Game_Player_Passing_Stats> home_Passing_stats = bs_rec.Game.Game_Player_Passing_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID && x.Pass_Att > 0)
                .OrderByDescending(x => x.Pass_Yards).ToList();

            List<Game_Player_Rushing_Stats> home_Rushing_stats = bs_rec.Game.Game_Player_Rushing_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID && x.Rush_Att > 0)
                .OrderByDescending(x => x.Rush_Yards).ToList();

            List<Game_Player_Receiving_Stats> home_Receiving_stats = bs_rec.Game.Game_Player_Receiving_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID && x.Rec_Catches > 0)
                .OrderByDescending(x => x.Rec_Yards).ToList();

            List<Game_Player_Pass_Defense_Stats> home_Pass_Defense_stats = bs_rec.Game.Game_Player_Pass_Defense_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID && (x.Ints > 0 || x.Def_Pass_Defenses > 0))
                .OrderByDescending(x => x.Ints).ThenByDescending(x => x.Def_Pass_Defenses).ToList();

            List<Game_Player_Defense_Stats> home_Defense_stats = bs_rec.Game.Game_Player_Defense_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID && x.Def_Tackles > 0)
                .OrderByDescending(x => x.Def_Tackles).ToList();

            List<Game_Player_Kicker_Stats> home_kicker_stats = bs_rec.Game.Game_Player_Kicker_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID)
                .OrderByDescending(x => x.FG_Made).ToList();

            List<Game_Player_Punter_Stats> home_punter_stats = bs_rec.Game.Game_Player_Punter_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID)
                .OrderByDescending(x => x.num_punts).ToList();

            List<Game_Player_Kick_Returner_Stats> home_KickoffReturns_stats = bs_rec.Game.Game_Player_Kick_Returner_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID)
                .OrderByDescending(x => x.Kickoffs_Returned).ToList();

            List<Game_Player_Punt_Returner_Stats> home_PuntReturns_stats = bs_rec.Game.Game_Player_Punt_Returner_Stats
                .Where(x => x.Franchise_ID == bs_rec.Game.Home_Team_Franchise_ID)
                .OrderByDescending(x => x.Punts_Returned).ToList();

        }
    }
}
