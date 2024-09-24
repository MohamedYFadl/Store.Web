using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Service.ProductService.Dtos;
using System.Collections.Generic;
namespace Store.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsNoTrackingAsync();
            var mappedBrands = _mapper.Map<IReadOnlyList< BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }
        

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsNoTrackingAsync();
            var mappedProducts = _mapper.Map< IReadOnlyList < ProductDetailsDto >>(products);
            return mappedProducts; 
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsNoTrackingAsync();
            var mappedTypes = _mapper.Map< IReadOnlyList < BrandTypeDetailsDto >>(types);
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? ProductId)
        {
            if (ProductId is null)
                throw new Exception("Id Is Null");

            var product = _unitOfWork.Repository<Product, int>().GetByIdAsync(ProductId.Value);

            if(product is null)
                throw new Exception("product Is not found");


            var mappedProduct = _mapper.Map< ProductDetailsDto >(product);
            return mappedProduct;

        }
    }
}
