using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightMonitoringEntities
{
    public class CommercialAirliner : IFlyable
    {
        public string FlightIdentifier { get; set; }
        public double CurrentAltitude { get; set; }
        public bool IsCommercial { get { return true; } }

        public CommercialAirliner() {

        }
    }
}
