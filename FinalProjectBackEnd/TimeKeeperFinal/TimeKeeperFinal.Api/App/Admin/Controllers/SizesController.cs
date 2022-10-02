using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.SizeDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SizesController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizesController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sizeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _sizeService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(SizePostDTO sizePostDTO)
        {
            await _sizeService.PostAsync(sizePostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id,SizePutDTO sizePutDTO)
        {
            await _sizeService.PutAsync(id,sizePutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _sizeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _sizeService.RestoreAsync(id);
            return NoContent();
        }
    }
}
