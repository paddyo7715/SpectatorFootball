using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectatorFootball.Enum
{
    public enum block_result
    {
        EVEN,
        BLOCKER_ADVANTAGE,
        TACKLER_ADVANTAGE,
        BLOCKER_DOMINATED,
        TACKLER_DOMINATED,
        NO_BLOCKER  //There was no blocker used to the tackle method
    }
}
