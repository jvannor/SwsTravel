using System;
using System.Collections.Generic;

namespace api.entities
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductCategoryClassifications = new HashSet<ProductCategoryClassification>();
        }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryDescrition { get; set; }

        public virtual ICollection<ProductCategoryClassification> ProductCategoryClassifications { get; set; }
    }
}
