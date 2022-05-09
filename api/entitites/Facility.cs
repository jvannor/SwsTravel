using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class Facility
    {
        public Facility()
        {
            InversePartOfFacility = new HashSet<Facility>();
            PassengerTransportationOfferingFacilityIdgoingToNavigations = new HashSet<PassengerTransportationOffering>();
            PassengerTransportationOfferingFacilityIdoriginatingFromNavigations = new HashSet<PassengerTransportationOffering>();
        }

        public int FacilityId { get; set; }
        public int? PartOfFacilityId { get; set; }
        public string FacilityDescription { get; set; }
        public string FacilityName { get; set; }
        public int FacilityTypeId { get; set; }
        public int? SquareFootage { get; set; }

        public virtual FacilityType FacilityType { get; set; }
        public virtual Facility PartOfFacility { get; set; }
        public virtual TransportationFacility TransportationFacility { get; set; }
        public virtual ICollection<Facility> InversePartOfFacility { get; set; }
        public virtual ICollection<PassengerTransportationOffering> PassengerTransportationOfferingFacilityIdgoingToNavigations { get; set; }
        public virtual ICollection<PassengerTransportationOffering> PassengerTransportationOfferingFacilityIdoriginatingFromNavigations { get; set; }
    }
}
