using System;
using System.Collections.Generic;

namespace api.entities
{    public partial class ShipPort
    {
        public int FacilityId { get; set; }
        public virtual TransportationFacility Facility { get; set; }
    }
}
