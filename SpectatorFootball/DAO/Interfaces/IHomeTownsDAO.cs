using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.DAO.Interfaces
{
    public interface IHomeTownsDAO
    {
        int getTotalHomeTowns();
        string getHomeTownbyRecNum(int rec_num);

    }
}
