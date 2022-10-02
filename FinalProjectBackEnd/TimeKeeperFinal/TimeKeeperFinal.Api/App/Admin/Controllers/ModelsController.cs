using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.ModelDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _modelService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _modelService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ModelPostDTO modelPostDTO)
        {
            await _modelService.PostAsync(modelPostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, ModelPutDTO modelPutDTO)
        {
            await _modelService.PutAsync(id, modelPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _modelService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _modelService.RestoreAsync(id);
            return NoContent();
        }
    }
}
