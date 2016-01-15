using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public enum RStates
    {
        Added=1,
        Updated=2,
        Deleted=3
    }

    public enum MaintanceStates
    {
        INCOMPLETE=1,
        DONE=2,
        MODEFIED=3
    }
    public enum InstallStates
    {
        INSTALLING=1,
        INSTALLED=2
    }
}
