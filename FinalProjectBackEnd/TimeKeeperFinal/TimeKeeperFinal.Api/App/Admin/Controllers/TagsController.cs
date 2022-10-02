using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.TagDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tagService.GetAllAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _tagService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(TagPostDTO tagPostDTO)
        {
            await _tagService.PostAsync(tagPostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, TagPutDTO tagPutDTO)
        {
            await _tagService.PutAsync(id, tagPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _tagService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _tagService.RestoreAsync(id);
            return NoContent();
        }
    }
}
