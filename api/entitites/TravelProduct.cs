using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class TravelProduct
    {
        public TravelProduct()
        {
            ProductCategoryClassifications = new HashSet<ProductCategoryClassification>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public virtual PassengerTransportationOffering PassengerTransportationOffering { get; set; }
        public virtual ICollection<ProductCategoryClassification> ProductCategoryClassifications { get; set; }
    }
}
