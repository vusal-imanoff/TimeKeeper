using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.ProductDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromForm] ProductPostDTO productPostDTO)
        {
            await _productService.PostAsync(productPostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromForm] ProductPutDTO productPutDTO)
        {
            await _productService.PutAsync(id, productPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _productService.RestoreAsync(id);
            return NoContent();
        }
    }
}
