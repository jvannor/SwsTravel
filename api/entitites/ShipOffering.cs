using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class ShipOffering
    {
        public int ProductId { get; set; }

        public virtual PassengerTransportationOffering Product { get; set; }
    }
}
