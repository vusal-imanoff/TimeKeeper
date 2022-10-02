using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IBlogService _blogService;

        public HomesController(ISliderService sliderService, IBlogService blogService)
        {
            _sliderService = sliderService;
            _blogService = blogService;
        }

        [HttpGet("slider")]
        public async Task<IActionResult> Sliders()
        {
            return Ok(await _sliderService.GetAllForUsersAsync());
        }


        [HttpGet("blog")]
        public async Task<IActionResult> Blogs()
        {
            return Ok(await _blogService.GetAllForUsersAsync());
        }

    }
}
