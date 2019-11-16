using System.Collections.Generic;
using log4net;

namespace SpectatorFootball
{
    public class StockTeams_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public List<TeamMdl> getAllStockTeams()
        {
            var r = new List<TeamMdl>();
            var StockTeamDAO = new Stock_TeamsDAO();
            r = StockTeamDAO.getAllStockTeams();
            return r;
        }
        public void AddStockTeam(TeamMdl Team)
        {
            logger.Info("Adding new stock team " + Team.Nickname);
            var StockTeamDAO = new Stock_TeamsDAO();
            StockTeamDAO.AddStockTeam(Team);
        }
        public void DeleteStockTeam(int t_id)
        {
            logger.Info("Deleting stock team with id " + t_id.ToString());
            var StockTeamDAO = new Stock_TeamsDAO();
            StockTeamDAO.DeleteStockTeam(t_id);
        }
        public void UpdateStockTeam(TeamMdl Team)
        {
            logger.Info("Updating stock team " + Team.Nickname);
            var StockTeamDAO = new Stock_TeamsDAO();
            StockTeamDAO.UpdateStockTeam(Team);
        }
        public bool DoesTeamAlreadyExist_ID(string City, string Nickname, string original_City, string original_Nickname)
        {
            return new Stock_TeamsDAO().DoesTeamAlreadyExist_ID(City, Nickname, original_City, original_Nickname);
        }
        public bool DoesTeamAlreadyExist(string City, string Nickname)
        {
            return new Stock_TeamsDAO().DoesTeamAlreadyExist(City, Nickname);
        }
    }
}
