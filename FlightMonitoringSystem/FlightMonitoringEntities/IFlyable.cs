using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightMonitoringEntities
{
    public interface IFlyable
    {
        string FlightIdentifier { get; set; }
        double CurrentAltitude { get; set; }
        bool IsCommercial { get; }
    }
}
