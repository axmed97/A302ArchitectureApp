using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public double Review { get; set; }
        public bool IsStock { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProductLanguage> ProductLanguages { get; set; }
        public ICollection<ProductPhoto> ProductPhotos { get; set; }
    }
}
