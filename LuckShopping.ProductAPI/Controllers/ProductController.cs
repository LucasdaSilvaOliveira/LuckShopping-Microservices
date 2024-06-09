using AutoMapper;
using LuckShopping.ProductAPI.DTOs;
using LuckShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var product = await _productRepository.FindAll();

            var productDTO = _mapper.Map<List<ProductDTO>>(product);

            return Ok(productDTO);
        }

        [HttpGet("{id}")]
        //[HttpGet("id")] = Nesse formato, o id não é required
        public async Task<IActionResult> FindById(int id)
        {
            var product = await _productRepository.FindById(id);

            var productDTO = _mapper.Map<ProductDTO>(product);

            return Ok(productDTO);
        }


    }
}
