using AutoMapper;
using LuckShopping.ProductAPI.Data.Entites;
using LuckShopping.ProductAPI.DTOs;
using LuckShopping.ProductAPI.Repository;
using LuckShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var product = await _productRepository.FindAll();

            var productDTO = _mapper.Map<List<ProductDTO>>(product);

            return Ok(productDTO);
        }

        [HttpGet("{id}")]
        [Authorize]
        //[HttpGet("id")] = Nesse formato, o id não é required
        public async Task<IActionResult> FindById(int id)
        {
            var product = await _productRepository.FindById(id);

            var productDTO = _mapper.Map<ProductDTO>(product);

            return Ok(productDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            var response = await _productRepository.Create(product);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProductDTO dto)
        {

            var product = _mapper.Map<Product>(dto);

            var response = await _productRepository.Update(product);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _productRepository.Delete(id);
            if(response) return Ok();

            return BadRequest(response);
        }
    }
}
