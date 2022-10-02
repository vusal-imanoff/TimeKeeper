using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.Interfaces;

namespace TimeKeeperFinal.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly ISizeService _sizeService;
        private readonly IColorService _colorService;

        public ProductsController(IProductService productService, ICategoryService categoryService, IBrandService brandService, ISizeService sizeService, IColorService colorService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _sizeService = sizeService;
            _colorService = colorService;
        }

        [HttpGet("getall/{pageIndex}")]
        public async Task<IActionResult> Index(int pageIndex=1)
        {
            return Ok(await _productService.GetAllPageIndexAsync(pageIndex));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        [HttpGet("colors")]
        public async Task<IActionResult> Colors()
        {
            return Ok(await _colorService.GetAllForUsersAsync());
        }

        [HttpGet("sizes")]
        public async Task<IActionResult> Sizes()
        {
            return Ok(await _sizeService.GetAllForUsersAsync());
        }

        [HttpGet("categories")]
        public async Task<IActionResult> Categories()
        {
            return Ok(await _categoryService.GetAllForUsersAsync());
        }

        [HttpGet("brands")]
        public async Task<IActionResult> Brands()
        {
            return Ok(await _brandService.GetAllForUsersAsync());
        }



    }
}
