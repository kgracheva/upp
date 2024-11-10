using Microsoft.AspNetCore.Mvc;
using upp.Dtos.Product;
using upp.Entities;
using upp.Mapper;
using upp.Services;

namespace upp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService) 
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromBody] ProductDto dto, CancellationToken token)
        {
            return Ok(await _productService.CreateProduct(dto, token));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetProducts([FromQuery] FindProductsDto dto, CancellationToken token)
        {
            return Ok(await _productService.GetProducts(dto, token));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDto>> EditProduct(int id, CancellationToken token)
        {
            return Ok(await _productService.GetProduct(id, token));
        }

        [HttpPut]
        public async Task<ActionResult<int>> EditProduct([FromBody] ProductDto dto, CancellationToken token)
        {
            return Ok(await _productService.EditProduct(dto, token));
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult> DeleteProduct(int id, CancellationToken token)
        {
            await _productService.Delete(id, token);
            return NoContent();
        }
    }
}
