using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class FacilityType
    {
        public FacilityType()
        {
            Facilities = new HashSet<Facility>();
        }

        public int FacilityTypeId { get; set; }
        public string FacilityTypeName { get; set; }
        public string FacilityDescription { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}
