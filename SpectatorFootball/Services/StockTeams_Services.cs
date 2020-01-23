using System.Collections.Generic;
using log4net;
using SpectatorFootball.Models;

namespace SpectatorFootball
{
    public class StockTeams_Services
    {
        private static ILog logger = LogManager.GetLogger("RollingFile");

        public List<Stock_Teams> getAllStockTeams()
        {
            var r = new List<Stock_Teams>();
            var StockTeamDAO = new Stock_TeamsDAO();
            r = StockTeamDAO.getAllStockTeams();
            return r;
        }
        public void AddStockTeam(Stock_Teams Team)
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
        public void UpdateStockTeam(Stock_Teams Team)
        {
            logger.Info("Updating stock team " + Team.Nickname);
            var StockTeamDAO = new Stock_TeamsDAO();
            StockTeamDAO.UpdateStockTeam(Team);
        }
        public bool DoesTeamAlreadyExist(string City, string Nickname)
        {
            return new Stock_TeamsDAO().DoesTeamAlreadyExist(City, Nickname);
        }
    }
}
