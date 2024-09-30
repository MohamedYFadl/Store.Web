using Store.Data.Entities;

namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductWithCountSpecs:BaseSpecification<Product>
    {
        public ProductWithCountSpecs(ProductSpecification specs)
    : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
    (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value))
        {
        
        }
    }
}
