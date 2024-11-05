using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using AutoMapper;
using upp.Entities;
using upp.Dtos.Product;


namespace upp.Mapper
{
    public class ProductMapper : Profile
    {
        public ProductMapper() {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
