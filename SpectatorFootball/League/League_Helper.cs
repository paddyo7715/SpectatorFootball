using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.League
{
    class League_Helper
    {

        public static SpectatorFootball.Models.League Clone_League(SpectatorFootball.Models.League l)
        {
            SpectatorFootball.Models.League r = new SpectatorFootball.Models.League();

            var sourceProperties = l.GetType().GetProperties();
            var destProperties = r.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(r, sourceProperty.GetValue(l));
                        break;
                    }
                }
            }

            return r;
        }

    }
}
