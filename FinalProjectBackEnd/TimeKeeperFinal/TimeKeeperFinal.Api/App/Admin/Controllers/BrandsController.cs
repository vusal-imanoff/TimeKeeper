using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.BrandDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BrandPostDTO brandPostDTO)
        {
            await _brandService.PostAsync(brandPostDTO);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _brandService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromForm] BrandPutDTO brandPutDTO)
        {
            await _brandService.PutAsync(id, brandPutDTO);

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _brandService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _brandService.RestoreAsync(id);
            return NoContent();
        }
    }
}
