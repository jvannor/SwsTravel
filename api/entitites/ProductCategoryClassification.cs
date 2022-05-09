using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class ProductCategoryClassification
    {
        public int ProductCategoryId { get; set; }
        public int ProductId { get; set; }
        public DateTime FromDate { get; set; }
        public string Comments { get; set; }
        public bool PrimaryFlag { get; set; }
        public DateTime ThruDate { get; set; }

        public virtual TravelProduct Product { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
