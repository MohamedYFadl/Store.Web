﻿namespace Store.Repository.Specification.ProductSpecs
{
    public class ProductSpecification
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 50;
        private int _pageSize = 6;

        public int PageSize
        {
            get =>  _pageSize; 
            set => _pageSize =(value > MaxPageSize) ? int.MaxValue : value; 
        }


    }
}
