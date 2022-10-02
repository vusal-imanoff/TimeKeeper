using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.SliderDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SlidersController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SlidersController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]SliderPostDTO sliderPostDTO)
        {
            await _sliderService.PostAsync(sliderPostDTO);

            return StatusCode(201);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sliderService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _sliderService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromForm]SliderPutDTO sliderPutDTO)
        {
            await _sliderService.PutAsync(id, sliderPutDTO);

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _sliderService.DeleteAsync(id);
            return StatusCode(204);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _sliderService.RestoreAsync(id);
            return StatusCode(204);
        }
    }
}
