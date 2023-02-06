using CoreAPI.Core.DTOs;
using CoreAPI.Core.Models;
using CoreAPI.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Libary.Dtos;

namespace CoreAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IGenericService<Product,ProductDto> _productService;

        public ProductsController(IGenericService<Product, ProductDto> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _productService.GetAllAsync());
        }

        //[ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           
            return CreateActionResult(await _productService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            return CreateActionResult(await _productService.AddAsync(productDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {            
            return CreateActionResult(await _productService.Update(productDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
           
            return CreateActionResult(await _productService.RemoveAsync(id));
        }
    }
}
