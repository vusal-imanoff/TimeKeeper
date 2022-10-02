using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.ProductItemDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ProductItemsController : ControllerBase
    {
        private readonly IProductItemService _productItemService;

        public ProductItemsController(IProductItemService productItemService)
        {
            _productItemService = productItemService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productItemService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _productItemService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProductItemPostDTO productItemPostDTO)
        {
            await _productItemService.PostAsync(productItemPostDTO);
            return StatusCode(201);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id,ProductItemPutDTO productItemPutDTO)
        {
            await _productItemService.PutAsync(id, productItemPutDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _productItemService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _productItemService.RestoreAsync(id);
            return NoContent();
        }
    } 
}
