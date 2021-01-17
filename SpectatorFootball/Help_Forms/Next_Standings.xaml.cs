using SpectatorFootball.Enum;
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

namespace SpectatorFootball.Help_Forms
{
    /// <summary>
    /// Interaction logic for Next_Standings.xaml
    /// </summary>
    public partial class Next_Standings : Window
    {
        public Next_Standings()
        {
            InitializeComponent();
        }

        public void SetContent(League_State ls)
        {

            switch (ls)
            {
                case League_State.Season_Started:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("This Season has just begun.");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to conduct the draft by selecting Draft from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Draft_Started:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("The Draft has started, but is not yet completed..");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to continue the draft by selecting Draft from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Draft_Completed:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("The Draft has Completed");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to conduct Free Agency by selecting Free Agency from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.FreeAgency_Started:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Free Agency has started, but is not yet completed");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to resume Free Agency by selecting Free Agency from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.FreeAgency_Completed:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Free Agency has Completed");
                        sb.Append("\n\n");
                        sb.Append("The next step would be for teams to go into Training Camp  by selecting Training Camp from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Training_Camp_Started:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Training Camp has started but is not yet completed");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to play regualer season games by selecting Schedule from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Training_Camp_Ended:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Training Camp has Completed");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to play regualer season games by selecting Schedule from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Regular_Season_in_Progress:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("The Regular Season is in Progress");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to continue to play regualer season games by selecting Schedule from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Regular_Season_Ended:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("The Regular Season has ended");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to play playoff games by selecting either Playoffs or Schedule from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Playoffs_In_Progress:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("The Playoffs are in Progress");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to continue to play playoff games by selecting either Playoffs or Schedule from the League Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Season_Ended:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("The Playoffs Have Ended");
                        sb.Append("\n\n");
                        sb.Append("The next step would be to end the season by selecting End Season from the Tasks Menu Item");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
                case League_State.Previous_Year:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("You are Looking at a Previous Year");
                        sb.Append("\n\n");
                        sb.Append("To return to the current year in your league, click on the Current button on the upper left of the screen");
                        txtNextContent.Text = sb.ToString();
                        break;
                    }
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
