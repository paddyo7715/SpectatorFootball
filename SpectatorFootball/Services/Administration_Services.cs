using System;
using System.Collections.Generic;
using log4net;
using SpectatorFootball.Models;

namespace SpectatorFootball
{
    public class Administration_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        // This service is called when the user elects to create new potential name(s) by going into the
        // Administration function and entering a first and/or last name or selecting a name file to create
        // new potential names for new players.
        public int AddPlayerNames(string FirstName, string LastName, string sFile)
        {
            var FirstNames = new List<Potential_First_Names>();
            var LastNames = new List<Potential_Last_Names>();


            int i = 0;
            var pnDAO = new Player_NamesDAO();
            System.IO.StreamReader srFileReader = null;
            string sInputLine = "";
            string FirstLastName = "";

            logger.Info("Adding Player Names.");

            if (!CommonUtils.isBlank(FirstName) && FirstName.Trim().Length > 1)
                FirstNames.Add(new Potential_First_Names { FirstName = CommonUtils.CapitalizeFirstLetter(FirstName.Trim()) });
            if (!CommonUtils.isBlank(LastName) && LastName.Trim().Length > 1)
                LastNames.Add(new Potential_Last_Names { LastName = CommonUtils.CapitalizeFirstLetter(LastName.Trim()) });

            if (!CommonUtils.isBlank(sFile))
            {
                srFileReader = System.IO.File.OpenText(sFile);

                do {
                    sInputLine = srFileReader.ReadLine();

                    if (sInputLine == "{FirstNames}")
                    {
                        FirstLastName = "F";
                        continue;
                    }
                    else if (sInputLine == "{LastNames}")
                    {
                        FirstLastName = "L";
                        continue;
                    }

                    if (CommonUtils.isBlank(sInputLine) || sInputLine.Trim().Length == 0)
                        continue;
                    switch (FirstLastName)
                    {
                        case "F":
                            {
                                FirstNames.Add(new Potential_First_Names { FirstName = CommonUtils.CapitalizeFirstLetter(sInputLine.Trim()) });
                                break;
                            }

                        case "L":
                            {
                                LastNames.Add(new Potential_Last_Names { LastName = CommonUtils.CapitalizeFirstLetter(sInputLine.Trim()) });
                                break;
                            }

                        default:
                            {
                                throw new Exception("No First or Last Name header in player names file.");
                            }
                    }
                } while (sInputLine != null);
            }

            if (FirstNames.Count > 0) i += pnDAO.AddFirstName(FirstNames);
            if (LastNames.Count > 0) i += pnDAO.AddLastName(LastNames);
            return i;
        }
        // This service is called to get the total number of potential first and last names for new players.
        public long[] getPlayerNameTotals()
        {
            var r = new long[2];
            var pnDAO = new Player_NamesDAO();

            r[0] = pnDAO.getTotalFirstNames();
            r[1] = pnDAO.getTotalLastNames();

            return r;
        }

        public string getRandomHomeTown()
        {
            string r = null;

            var htDAO = new HomeTownsDAO();
            int tot_hometowns = htDAO.getTotalHomeTowns();

            int tot_range = 0;
            for (int i = 1; i <= tot_hometowns; i++)
                tot_range += (tot_hometowns - i) + 1;

            int rnd = CommonUtils.getRandomNum(1, tot_range);

            int record_num = 0;
            for (int i = 1; i <= tot_hometowns; i++)
            {
                record_num += (tot_hometowns - i) + 1;
                if (rnd <= record_num)
                {
                    record_num = i;
                    break;
                }
            }

            r = htDAO.getHomeTownbyRecNum(record_num);

            return r;
        }
    }
}
