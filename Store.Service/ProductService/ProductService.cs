using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.ProductService.Dtos;
namespace Store.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();
            var mappedBrands = brands.Select(x => new BrandTypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreateAt,
            }).ToList();
            return mappedBrands;
        }
        

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsNoTrackingAsync();
            var mappedProducts = products.Select(x => new ProductDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreateAt,
                BrandName = x.Brand.Name,
                TypeName = x.Type.Name,
                PictureUrl = x.PictureUrl,
                Price = x.Price,
            }).ToList();
            return mappedProducts; 
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();
            var mappedTypes = types.Select(x => new BrandTypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreateAt,
            }).ToList();
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? ProductId)
        {
            if (ProductId is null)
                throw new Exception("Id Is Null");

            var product = _unitOfWork.Repository<Product, int>().GetByIdAsync(ProductId.Value);

            if(product is null)
                throw new Exception("product Is not found");


            var mappedProduct = new ProductDetailsDto {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                CreatedAt = product.CreatedAt,
                Description = product.Description,
                BrandName= product.Brand.Name,
                TypeName = product.Type.Name,
                
                
            };
            return mappedProduct;

        }
    }
}
