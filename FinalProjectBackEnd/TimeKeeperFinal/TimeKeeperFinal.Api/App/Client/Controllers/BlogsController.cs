using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _blogService.GetAllForUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            return Ok(await _blogService.GetByIdAsync(id));
        }
    }
}
