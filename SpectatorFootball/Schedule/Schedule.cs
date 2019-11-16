using System.Collections.Generic;
using System;

namespace SpectatorFootball
{
    public class Schedule
    {
        private string League;
        private int Teams;
        private int TeamsperDiv;
        private int Conferences;
        private int Weeks;
        private int byes;
        private List<string> sched = new List<string>();
        private Random rha = new Random();

        public Schedule(string Short_Name, int Number_of_Teams, int Num_Teams_Per_Division, int Conferences, int weeks, int byes)
        {
            League = Short_Name;
            Teams = Number_of_Teams;
            TeamsperDiv = Num_Teams_Per_Division;
            this.Conferences = Conferences;
            Weeks = weeks;
            this.byes = byes;
        }

        private int getDivision(int Team)
        {
            return (Team - 1) / TeamsperDiv + 1;
        }

        private int getConference(int Team)
        {
            if (Conferences == 0)
                return 1;
            else
                return (Team - 1) / (Teams / Conferences) + 1;
        }

        private int[] game_type_fianl_sched(List<string> v, int T)
        {
            int home_games = 0;
            int away_games = 0;
            int nondiv_conf_games = 0;
            int nonconf_games = 0;

            foreach (string lv in v)
            {
                string g = lv;
                string[] m = g.Split(',');
                if (m[1] == T.ToString())
                {
                    home_games = home_games + 1;
                    if (getConference(int.Parse(m[1].ToString())) == getConference(int.Parse(m[2].ToString())) && getDivision(int.Parse(m[1].ToString())) != getDivision(int.Parse(m[2].ToString())))
                        nondiv_conf_games = nondiv_conf_games + 1;

                    if (getConference(int.Parse(m[1].ToString())) != getConference(int.Parse(m[2].ToString())))
                        nonconf_games = nonconf_games + 1;
                }

                if (m[2] == T.ToString())
                {
                    away_games = away_games + 1;
                    if (getConference(int.Parse(m[2])) == getConference(int.Parse(m[1])) && getDivision(int.Parse(m[2])) != getDivision(int.Parse(m[1])))
                        nondiv_conf_games = nondiv_conf_games + 1;

                    if (getConference(int.Parse(m[2])) != getConference(int.Parse(m[1])))
                        nonconf_games = nonconf_games + 1;
                }
            }

            return new int[] { home_games, away_games, nondiv_conf_games, nonconf_games };
        }

        private int[] game_types(int T)
        {
            int home_games = 0;
            int away_games = 0;
            int nondiv_conf_games = 0;
            int nonconf_games = 0;

            foreach (string lv in sched)
            {
                string g = lv;
                string[] m = g.Split(',');
                if (m[0] == T.ToString())
                {
                    home_games = home_games + 1;
                    if (getConference(int.Parse(m[0].ToString())) == getConference(int.Parse(m[1].ToString())) && getDivision(int.Parse(m[0].ToString())) != getDivision(int.Parse(m[1].ToString())))
                        nondiv_conf_games = nondiv_conf_games + 1;

                    if (getConference(int.Parse(m[0].ToString())) != getConference(int.Parse(m[1].ToString())))
                        nonconf_games = nonconf_games + 1;
                }

                if (m[1] == T.ToString())
                {
                    away_games = away_games + 1;
                    if (getConference(int.Parse(m[1].ToString())) == getConference(int.Parse(m[0].ToString())) && getDivision(int.Parse(m[1].ToString())) != getDivision(int.Parse(m[0].ToString())))
                        nondiv_conf_games = nondiv_conf_games + 1;

                    if (getConference(int.Parse(m[1].ToString())) != getConference(int.Parse(m[0].ToString())))
                        nonconf_games = nonconf_games + 1;
                }
            }

            return new int[] { home_games, away_games, nondiv_conf_games, nonconf_games };
        }

        public bool DupeGame(int Home, int Away)
        {
            bool r = false;

            foreach (string lv in sched)
            {
                string g = lv;
                string[] m = g.Split(',');

                if ((m[0] == Home.ToString() || m[0] == Away.ToString()) &&
                    (m[1] == Home.ToString() || m[1] == Away.ToString()))
                {
                    r = true;
                    break;
                }
            }

            return r;
        }

        public List<string> Generate_Regular_Schedule()
        {
            List<string> Weekly_sched;
            int expected_home_games;
            int expected_away_games;
            int i;
            int tries;

        START_METHOD:
            ;
            int[] gt = null;

            Weekly_sched = new List<string>();
            Weekly_sched.Add("Week Number,Home Team ID,Away Team ID");
            expected_home_games = Weeks / 2;
            expected_away_games = expected_home_games;
            i = 1;
            tries = 0;

            // Create division games
            while (i <= Teams)
            {
                int cur_div = getDivision(i);
                int beg_div = cur_div * TeamsperDiv + (1 - TeamsperDiv);
                int cur_conf = getConference(i);
                int x = beg_div - 1;
                while (x < beg_div + TeamsperDiv - 1)
                {
                    x += 1;
                    if (i == x)
                        continue;

                    if (DupeGame(i, x))
                        continue;

                    sched.Add(i.ToString() + "," + x.ToString());
                    sched.Add(x.ToString() + "," + i.ToString());
                }

                i += 1;
            }

            // file.WriteLine("Finished scheduling div games")

            // Create all other games        
            // 
            double num_of_weeks = (Teams / (double)2 * Weeks - Teams * (TeamsperDiv - 1)) / (Teams / (double)2);
            int wg_count = 0;
            while (wg_count < num_of_weeks)
            {
                int t;
                int ii;
                int wgcount = Teams / 2;
                string[] wgames = new string[wgcount - 1 + 1];
                int wg_now = 0;
                // clear the weekly games    
                int wg_ind = 0;
                foreach (string s in wgames) {
                    wgames[wg_ind] = null;
                    wg_ind++;
                }

                tries = 0;
                // Dim rha As Random = New Random()
                while (true)
                {
                    int home = 0;
                    int away = 0;
                    ii = rha.Next(Teams) + 1;
                    t = rha.Next(Teams) + 1;

                    tries += 1;

                    if (tries == 2000)
                    {
                        wgames = new string[wgcount - 1 + 1];
                        wg_now = 0;
                        tries = 0;
                    }

                    // if teams are equal then pick new teams.
                    if (ii == t)
                        continue;

                    // if same division then pick new teams.
                    if (getDivision(ii) == getDivision(t))
                        continue;

                    gt = game_types(ii);
                    int a = gt[0] - gt[1];
                    gt = game_types(t);
                    int b = gt[0] - gt[1];
                    if (a < b)
                    {
                        home = ii;
                        away = t;
                    }
                    else
                    {
                        home = t;
                        away = ii;
                    }

                    string pgame = home.ToString() + "," + away.ToString();
                    if (!dupe_weekly_game(wgames, pgame))
                    {
                        wgames[wg_now] = pgame;
                        wg_now += 1;
                    }

                    if (wg_now == wgcount)
                        break;
                }

                sched.AddRange(wgames);
                // file.WriteLine(("Number of Games Scheduled " + sched.Count.ToString))
                wg_count += 1;
            }

            // Make sure that all teams have an even number of home and away games.
            tries = 0;
        EVEN_HOME_AND_AWAY:
            ;
            int g = 0;
            for (i = 1; i <= Teams; i++)
            {
                while (1 == 1)
                {
                    List<int> Even_non_Div = new List<int>();
                    bool need_to_flip_even_other = true;

                    if (tries >= 2000)
                        // file.WriteLine("trying to even out teams failed, starting completely over.")
                        goto START_METHOD;

                    tries += 1;

                    int[] g1 = game_types(i);
                    int ha_diff_i = g1[0] - g1[1];

                    // if the team has an too many home games then continue.
                    if (ha_diff_i <= 0)
                    {
                        need_to_flip_even_other = false;
                        break;
                    }

                    // go thru the games and try to find a game for this team where they play another team that has 
                    // the opposite home/away defecit
                    g = 0;
                    while (g < sched.Count - 1)
                    {
                        int other_team = 0;
                        string[] p_game = sched[g].Split(',');
                        int h = int.Parse(p_game[0]);
                        int a = int.Parse(p_game[1]);

                        if (h == i)
                            other_team = int.Parse(p_game[1]);
                        else if (a == i)
                            other_team = int.Parse(p_game[0]);

                        // if the team i is not either the home or away store the game to possibly 
                        // shift if no other actual candidate is found then check the next game
                        if (other_team == 0)
                        {
                            g += 1;
                            continue;
                        }

                        // if the two teams are in the same division then the game can not be flipped.
                        if (getDivision(i) == getDivision(other_team))
                        {
                            g += 1;
                            continue;
                        }

                        int[] other_team_totals = game_types(other_team);

                        int other_diff = other_team_totals[0] - other_team_totals[1];

                        // if the other team has an even number of home and away then write to list of
                        // possible even games to home and away flip with this game if a better candidate
                        // is not found.
                        if (other_diff == 0 && h == i)
                            Even_non_Div.Add(g);

                        // if the other team has an imbalace of home and away games in the other direction then
                        // swap the home and away game.
                        if (h == i && ha_diff_i > 0 && other_diff < 0)
                        {
                            need_to_flip_even_other = false;
                            sched[g] = a.ToString() + "," + h.ToString();
                            // file.WriteLine("home and away switched to " & a.ToString & "," & h.ToString)
                            break;
                        }
                        g += 1;
                    } // Do While g < sched.Count() - 1

                    // If this team (i) had two many home games but couldn't find a game with a team with too many 
                    // away games to flip then flip a random game with an opponent that has an even number of home
                    // and away games and then start the home/away evening out all over.

                    if (need_to_flip_even_other)
                    {
                        if (Even_non_Div.Count == 0)
                            // file.WriteLine("no even team to flip as last resort.  Starting whole process again")
                            goto START_METHOD;

                        int rand_g = CommonUtils.getRandomNum(0, Even_non_Div.Count - 1);
                        string[] p_game = sched[Even_non_Div[rand_g]].Split(',');
                        int h = int.Parse(p_game[0]);
                        int a = int.Parse(p_game[1]);
                        sched[Even_non_Div[rand_g]] = a.ToString() + "," + h.ToString();
                        // file.WriteLine("game " & rand_g & " changed to " & a.ToString & "," & h.ToString)
                        goto EVEN_HOME_AND_AWAY;
                    }
                } // Do While 1 = 1
            }

            // file.WriteLine(("Finished scheduling all other games sched size " + sched.Count.ToString))
            // Print out game totals for each team
            // file.WriteLine("Game totals:")
            int yyu = 1;
            while (yyu <= Teams)
            {
                int[] zt = game_types(yyu);
                // file.WriteLine(yyu.ToString + " " + zt(0).ToString + " " + zt(1).ToString + " " + zt(2).ToString + " " + zt(3).ToString)
                yyu += 1;
            }

            // schedule weekly games            
            int w = 1;
            while (w <= Weeks + byes)
            {
                if (sched.Count == 0)
                    break;

                // file.WriteLine(("Week " + w.ToString))
                int games = Teams / 2;
                int wi = 0;
                string[] week_arr = new string[games - 1];
                string pg = null;
                tries = 0;

                while (true)
                {
                    tries += 1;
                    if (tries >= 50000)
                    {
                        tries = 0;
                        // file.WriteLine("tries exceed If (tries >= 50000) starting over")
                        break;
                    }

                    int gg = rha.Next(sched.Count) + 1;

                    gg -= 1;

                    pg = sched[gg];

                    if (!dupe_weekly_game(week_arr, pg))
                    {
                        sched.RemoveAt(gg);
                        week_arr[wi] = pg;
                        wi = wi + 1;
                        tries = 0;
                    }

                    if (wi == games || sched.Count == 0)
                        break;
                }

                // Move week to overall sched vector
                foreach (string wkq in week_arr)
                {
                    if (!(wkq == null))
                        Weekly_sched.Add(w.ToString() + "," + wkq);
                }
                w = w + 1;
            }

            // file.WriteLine("Scheduling left over games")
            tries = 0;
            while (sched.Count > 0)
            {
                // file.WriteLine(("Left over games: " + sched.Count.ToString))
                Random r2 = new Random();
                int lo_rand = r2.Next(sched.Count);
                string lo_game = null;

                tries += 1;
                if (tries >= 200000)
                    // file.WriteLine("exceeded tries >= 200000) goto outer ")
                    goto OUTER_DO;

                int w3 = 1;
                while (w3 <= Weeks + byes)
                {
                    lo_game = sched[lo_rand];

                    if (sched_games_for_week(w3, Weekly_sched) == Teams / 2)
                    {
                        w3 += 1;
                        continue;
                    }

                    string[] t2 = lo_game.Split(',');
                    string h = sched_weekly_team_game(w3, t2[0], Weekly_sched, true);
                    string a = sched_weekly_team_game(w3, t2[1], Weekly_sched, false);
                    if (h == null || a == null)
                    {
                        int bw = r2.Next(2) + 1;
                        if (bw == 2)
                        {
                            w3 += 1;
                            continue;
                        }

                        // to fix the issue of the null pointer exception caused by both h and a being nothing
                        if (h == null && a == null)
                        {
                            w3 += 1;
                            continue;
                        }

                        string ng = null;
                        if (h != null)
                            ng = h;
                        else
                            ng = a;

                        int n_index = sched_game_index(ng, Weekly_sched);
                        swap_game(w3, lo_game, ng, sched, lo_rand, Weekly_sched, n_index);
                        int www = Weeks + byes;
                        while (www > 0)
                        {
                            string[] yy = ng.Split(',');
                            string hh = sched_weekly_team_game(www, yy[1], Weekly_sched, true);
                            string aa = sched_weekly_team_game(www, yy[2], Weekly_sched, false);
                            if (hh == null && aa == null)
                            {
                                add_game_to_weekly_sched(Weekly_sched, www, ng);
                                sched.RemoveAt(lo_rand);
                                goto OUTER_DO;
                            }

                            www -= 1;
                        }
                    }

                    w3 += 1;
                }

            OUTER_DO:
                ;
            }

            return Weekly_sched;
        }

        private void add_game_to_weekly_sched(List<string> v, int w, string game)
        {
            string[] u = game.Split(',');
            int qei = 0;
            while (qei < v.Count)
            {
                string q = v[qei];
                string[] gt = q.Split(',');
                if (gt[0] == w.ToString())
                {
                    v.Insert(qei, gt[0].ToString() + "," + u[1] + "," + u[2]);
                    break;
                }

                qei = qei + 1;
            }
        }

        private void swap_game(int week, string lef_over_game, string scheded_game, List<string> sched, int sched_index, List<string> weekly_sched, int ws_index)
        {
            string[] gt = scheded_game.Split(',');
            sched[sched_index] = gt[1] + "," + gt[2];
            weekly_sched[ws_index] = week.ToString() + "," + lef_over_game;
        }

        private int sched_game_index(string g, List<string> v)
        {
            int r = -1;
            int qei = 0;
            while (qei < v.Count)
            {
                string q = v[qei];
                if (g == q)
                {
                    r = qei;
                    break;
                }

                qei += 1;
            }

            return r;
        }

        private string sched_weekly_team_game(int x, string t, List<string> v, bool home)
        {
            string r = null;
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

                if ((qt[0] == x.ToString()) && (qt[1] == t || qt[2] == t))
                {
                    r = q;
                    break;
                }

                qei += 1;
            }

            return r;
        }

        private int sched_games_for_week(int x, List<string> v)
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

                if (qt[0] == x.ToString())
                    r += 1;

                qei += 1;
            }

            return r;
        }

        private bool dupe_weekly_game(string[] w, string g)
        {
            bool r = false;
            string[] m = g.Split(',');
            int g1 = int.Parse(m[0]);
            int g2 = int.Parse(m[1]);
            int i = 0;
            while (i < w.Length)
            {
                if (w[i] == null)
                    break;

                string[] m2 = w[i].Split(',');
                int w1 = int.Parse(m2[0]);
                int w2 = int.Parse(m2[1]);
                if (g1 == w1 || g1 == w2)
                {
                    r = true;
                    break;
                }

                if (g2 == w1 || g2 == w2)
                {
                    r = true;
                    break;
                }

                i = i + 1;
            }

            return r;
        }
    }
}
