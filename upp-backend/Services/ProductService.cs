using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using upp.Dtos;
using upp.Dtos.Product;
using upp.Entities;
using upp.Mapper;

namespace upp.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateProduct(ProductDto dto, CancellationToken token)
        {
            if (dto.Id != 0)
                throw new Exception("product Id is not null!");

            var product = _mapper.Map<Product>(dto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync(token);

            return product.Id;
        }

        public async Task<int> EditProduct(ProductDto dto, CancellationToken token)
        {
            if (dto.Id == 0)
                throw new Exception("product Id is null!");

            var product = _context.Products.FirstOrDefault(x => x.Id == dto.Id);

            if(product == null)
                throw new Exception("product Id is null!");

            product = _mapper.Map(dto, product);

            _context.Products.Update(product);

            _context.Products.Update(product);
            await _context.SaveChangesAsync(token);

            return product.Id;
        }

        public async Task<PaginatedList<ProductDto>> GetProducts(FindProductsDto dto, CancellationToken token)
        {
            IQueryable<Product> query = _context.Products
                .Include(p => p.Creator)
                .ThenInclude(c => c.Info);

            if (dto.SearchName != "")
            {
                query = query.Where(p => p.Name.ToUpper().Contains(dto.SearchName.ToUpper()));
            }

            if(dto.CreatorId != 0)
            {
                query = query.Where(p => dto.CreatorId == p.CreatorId);
            }

            query = query.OrderBy(p => p.Name);

            return await query.ToPaginateListAsync<Product, ProductDto>(_mapper, dto.Page, dto.Size, token);
        }

        public async Task<ProductDto> GetProduct(int id, CancellationToken token)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, token);

            if(product == null)
            {
                throw new Exception("Product is null");
            }

            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(g => g.Id == id, token);

            if (product == null) throw new Exception("Product is null");

            product.IsDeleted = true;
            _context.Products.Update(product);

            await _context.SaveChangesAsync(token);
        }
    }
}
