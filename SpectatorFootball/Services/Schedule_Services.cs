using SpectatorFootball.DAO;
using SpectatorFootball.GameNS;
using SpectatorFootball.League;
using SpectatorFootball.Models;
using SpectatorFootball.Playoffs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Services
{
    class Schedule_Services
    {
        public List<Sched_Week_With_Name> GetSchedWeeks(long Season_ID, string League_Shortname, 
            long conferences, long PlayoffTeams, string ChampGameName)
        {
            List<Sched_Week_With_Name> r = new List<Sched_Week_With_Name>();

            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;
            //get number of possible playoffs weeks
            int playoffs_week = Playoff_Helper.NumPlayoffWeeks(PlayoffTeams);

            //First get the weeks in the schedule
            ScheduleDAO schedDAO = new ScheduleDAO();
            List<long> yearList = schedDAO.getYearinSched(Season_ID, League_con_string);

            //Next get a structure of wanted positions, in order, and unwanted positions for the team
            foreach (long i in yearList)
            {
                Sched_Week_With_Name sw = new Sched_Week_With_Name();
                sw.iWeek = i;
                if (i < app_Constants.PLAYOFF_WIDLCARD_WEEK_1)
                    sw.sWeek = "Week " + i;
                else
                    sw.sWeek = Playoff_Helper.GetPlayoffGameName(i, conferences, playoffs_week, ChampGameName);
                r.Add(sw);
            }

            //Now set the current week
            long currentWeek = schedDAO.getCurrentWeek(Season_ID, League_con_string);
            foreach (Sched_Week_With_Name w in r)
            {
                if (w.iWeek == currentWeek)
                    w.Current_Week = true;
                else
                    w.Current_Week = false;
            }

            return r;

        }

        public List<WeeklyScheduleRec> getWeeklySched(Loaded_League_Structure lls, long week)
        {
            string League_Shortname = lls.season.League_Structure_by_Season.First().Short_Name;
            string DIRPath_League = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname.ToUpper();
            string League_con_string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + app_Constants.GAME_DOC_FOLDER + Path.DirectorySeparatorChar + League_Shortname + Path.DirectorySeparatorChar + League_Shortname + "." + app_Constants.DB_FILE_EXT;

            ScheduleDAO sDAO = new ScheduleDAO();
            List<WeeklyScheduleRec> r = sDAO.getWeeklySched(lls.season.ID, week, League_con_string);

            foreach (WeeklyScheduleRec srec in r)
            {
                //Set the Game status for the weekly schedule
                if (srec.Game_Complete)
                {
                    srec.Status = "FINAL";
                    if (srec.QTR > app_Constants.QTRS_IN_REGULATION)
                        srec.Status += " (OT)";

                    srec.Action = "Box Score";
                }
                else
                {
                    if (srec.QTR == null)
                    {
                        srec.Status = "";
                        srec.Action = "Play";
                    }
                    else
                    {
                        srec.Status = Game_Helper.getQTRTime(srec.QTR, srec.QTR_Time);
                        srec.Action = "Resume";
                    }
                }

                string sAwayRecord = "(" + lls.getTeamStandings(srec.Away_Team_Name) + ")";
                string sHomeRecord = "(" +lls.getTeamStandings(srec.Home_Team_Name) + ")";

                srec.Away_Team_Name = sAwayRecord + " " + srec.Away_Team_Name;
                srec.Home_Team_Name += " " + sHomeRecord;

                srec.Away_HelmetImage = lls.getHelmetImg(srec.Away_helmet_filename);
                srec.Home_HelmetImage = lls.getHelmetImg(srec.Home_helmet_filename);
            }


            return r;
        }

        }
}
