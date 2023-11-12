using SpectatorFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DAO.Interfaces
{
    public interface IPlayer_NamesDAO
    {
        int AddFirstName(List<Potential_First_Names> FirstNames);
        int AddLastName(List<Potential_Last_Names> LastNames);
        long getTotalFirstNames();
        long getTotalLastNames();
        string[] CreatePlayerName();


    }
}
