using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class PassengerTransportationOffering
    {
        public int ProductId { get; set; }
        public int? FacilityIdgoingTo { get; set; }
        public int? FacilityIdoriginatingFrom { get; set; }
        public virtual Facility FacilityIdgoingToNavigation { get; set; }
        public virtual Facility FacilityIdoriginatingFromNavigation { get; set; }
        public virtual TravelProduct Product { get; set; }
        public virtual ShipOffering ShipOffering { get; set; }
    }
}
