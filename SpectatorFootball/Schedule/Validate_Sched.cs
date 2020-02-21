using System.Collections.Generic;
using System;

namespace SpectatorFootball
{
    public class Validate_Sched
    {
        private string League;
        private int Teams;
        private int TeamsperDiv;
        private int Conferences;
        private int Weeks;
        private int byes;

        public Validate_Sched(string Short_Name, int Number_of_Teams, int Num_Teams_Per_Division, int Conferences, int weeks, int byes)
        {
            League = Short_Name;
            Teams = Number_of_Teams;
            TeamsperDiv = Num_Teams_Per_Division;
            this.Conferences = Conferences;
            Weeks = weeks;
            this.byes = byes;
        }

        public string Validate(List<string> sched)
        {
            string r = null;

            try
            {
                //Make sure number of weeks in schedule is correct.
                string lastgame = sched[sched.Count - 1];
                string[] mtemp = lastgame.Split(',');
                if (mtemp[0] != (Weeks + byes).ToString())
                    throw new Exception("Invalid schedule created:  Incorrect number of weeks scheduled." + (Weeks + byes).ToString() + " games expected, but " + mtemp[0] + " games were scheduled");

                int expected_games = Teams / 2 * Weeks;
                int Actual_Games = sched.Count - 1;
                if (Actual_Games != expected_games)
                    throw new Exception("Invalid schedule created:  Incorrect number of league games scheduled." + expected_games.ToString() + " games expected, but " + Actual_Games.ToString() + " games were scheduled");

                for (int i = 1; i <= Teams; i++)
                {
                    int total_games = default(int); ;
                    int div_games = default(int); ;
                    int home_games = default(int);
                    int away_games = default(int);
                    int home_div_games = default(int);
                    int away_div_games = default(int);

                    total_games = 0;
                    div_games = 0;

                    foreach (string g in sched)
                    {
                        string[] m = g.Split(',');
                        string sWeek = m[0];
                        string ht = m[1];
                        string at = m[2];

                        if (sWeek.StartsWith("Week"))
                            continue;

                        if (ht == i.ToString() || at == i.ToString())
                        {
                            total_games += 1;

                            if (ht == i.ToString())
                                home_games += 1;
                            else if (at == i.ToString())
                                away_games += 1;

                            if (getDivision(int.Parse(ht.ToString())) == getDivision(int.Parse(at.ToString())))
                            {
                                if (ht == i.ToString())
                                    home_div_games += 1;
                                else if (at == i.ToString())
                                    away_div_games += 1;

                                div_games += 1;
                            }
                        }
                    }

                    if (total_games != Weeks)
                        throw new Exception("Schedule Error: Invalid number of games for team " + i.ToString());

                    if (div_games < (TeamsperDiv - 1) * 2)
                        throw new Exception("Schedule Error: Invalid number of divisional games for team " + i.ToString() + " ");

                    if (home_div_games / (double)(TeamsperDiv - 1) != 1.0)
                        throw new Exception("Schedule Error: Team " + i.ToString() + " does not have " + Convert.ToString(TeamsperDiv * 2) + "home divisional games schedule");

                    if (away_div_games / (double)(TeamsperDiv - 1) != 1.0)
                        throw new Exception("Schedule Error: Team " + i.ToString() + " does not have " + Convert.ToString(TeamsperDiv * 2) + "away divisional games schedule");

                    if ((home_games * 2.0) != (double)Weeks)
                        throw new Exception("Schedule Error: Team " + i.ToString() + " does not have " + Convert.ToString(Weeks / 2) + "home games schedule");

                    if ((away_games * 2.0) != (double)Weeks)
                        throw new Exception("Schedule Error: Team " + i.ToString() + " does not have " + Convert.ToString(Weeks / 2) + "away games schedule");

                    for (int w = 1; w <= Weeks + byes; w++)
                    {
                        if (sched_weekly_team_game(w, i.ToString(), sched) > 1)
                            throw new Exception("Schedule Error: Team " + i.ToString() + " is scheduled to play more than 1 game in week " + w.ToString());
                    }
                }

                // A nothing in r indicates successful validation
                return r;
            }
            catch (Exception ex)
            {
                // send back the error message
                return ex.Message;
            }
        }
        private int games_this_week(int week, int t, List<string> sched)
        {
            int r = 0;

            foreach (string g in sched)
            {
                string[] m = g.Split(',');
                if (m[0] == week.ToString())
                {
                    if (m[1] == t.ToString() || m[2] == t.ToString())
                        r += 1;
                }
            }

            return r;
        }

        private int getDivision(int Team)
        {
            return (Team - 1
                       ) / TeamsperDiv
                       + 1;
        }

        private int sched_weekly_team_game(int x, string t, List<string> v)
        {
            int r = 0;
            int qei = 0;
            while (qei < v.Count)
            {
                string q = v[qei];
                string[] qt = q.Split(',');
                if (qt[0] == "Week Number")
                {
                    qei += 1;
                    continue;
                }

                if (qt[0] == x.ToString() && (qt[1] == t || qt[2] == t))
                    r += 1;

                qei += 1;
            }

            return r;
        }
    }
}
