using System;
using log4net;

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
            int i = 0;
            var pnDAO = new Player_NamesDAO();
            System.IO.StreamReader srFileReader = null;
            string sInputLine = "";
            string FirstLastName = "";

            logger.Info("Adding Player Names.");

            if (!CommonUtils.isBlank(FirstName) && FirstName.Trim().Length > 1)
                i += pnDAO.AddFirstName(CommonUtils.CapitalizeFirstLetter(FirstName.Trim()));
            if (!CommonUtils.isBlank(LastName) && LastName.Trim().Length > 1)
                i += pnDAO.AddLastName(CommonUtils.CapitalizeFirstLetter(LastName.Trim()));

            if (!CommonUtils.isBlank(sFile))
            {
                srFileReader = System.IO.File.OpenText(sFile);
                sInputLine = srFileReader.ReadLine();
                if (sInputLine == "{FirstNames}")
                    FirstLastName = "F";
                else if (sInputLine == "{LastNames}")
                    FirstLastName = "L";
                while (sInputLine != null)
                {
                    sInputLine = srFileReader.ReadLine();
                    if (CommonUtils.isBlank(sInputLine) || sInputLine.Trim().Length == 0)
                        continue;
                    switch (FirstLastName)
                    {
                        case "F":
                            {
                                i += pnDAO.AddFirstName(CommonUtils.CapitalizeFirstLetter(sInputLine.Trim()));
                                break;
                            }

                        case "L":
                            {
                                i += pnDAO.AddLastName(CommonUtils.CapitalizeFirstLetter(sInputLine.Trim()));
                                break;
                            }

                        default:
                            {
                                throw new Exception("No First or Last Name header in player names file.");
                            }
                    }
                }
            }

            return i;
        }
        // This service is called to get the total number of potential first and last names for new players.
        public long[] getPlayerNameTotals()
        {
            var r = new long[3];
            var pnDAO = new Player_NamesDAO();

            r[0] = pnDAO.getTotalFirstNames();
            r[1] = pnDAO.getTotalLastNames();

            return r;
        }
    }
}
