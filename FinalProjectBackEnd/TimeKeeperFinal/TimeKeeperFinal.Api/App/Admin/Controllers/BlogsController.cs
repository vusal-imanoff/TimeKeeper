using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.BlogDTO;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm]BlogPostDTO blogPostDTO)
        {
            await _blogService.PostAsync(blogPostDTO);

            return StatusCode(201);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _blogService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _blogService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id,[FromForm] BlogPutDTO blogPutDTO)
        {
            await _blogService.PutAsync(id, blogPutDTO);

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _blogService.DeleteAsync(id);
            return StatusCode(204);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _blogService.RestoreAsync(id);
            return StatusCode(204);
        }
    }
}
