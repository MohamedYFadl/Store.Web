using Store.Data.Entities;

namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductWithSpecs : BaseSpecification<Product>
    {
        public ProductWithSpecs(ProductSpecification specs)
            : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
            (!specs.TypeId.HasValue || product.BrandId == specs.TypeId.Value))
        {
            AddInclude(x => x.BrandId);
            AddInclude(x => x.TypeId);
        }
    }
}
