using Entities.Common;

namespace Entities.Concrete
{
    public class ProductPhoto : BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
