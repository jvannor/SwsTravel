using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class TransportationFacility
    {
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual ShipPort ShipPort { get; set; }
    }
}
