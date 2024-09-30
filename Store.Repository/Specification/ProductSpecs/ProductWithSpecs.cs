using Store.Data.Entities;

namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductWithSpecs : BaseSpecification<Product>
    {
        public ProductWithSpecs(ProductSpecification specs)
            : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value) &&
            (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value))
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(specs.Sort)) 
            {
                switch (specs.Sort) 
                {
                    case "PriceAsc":
                        AddOrderBy(x=>x.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;

                }
            }
        }
        public ProductWithSpecs(int? Id): base(product =>product.Id == Id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
          
        }
    }
}
