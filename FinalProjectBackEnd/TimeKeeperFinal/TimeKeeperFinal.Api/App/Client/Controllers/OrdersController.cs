using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.OrderDTO;
using TimeKeeperFinal.Service.Interfaces;
using TimeKeeperFinal.Service.JwtManager.Interfaces;

namespace TimeKeeperFinal.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IJwtManager _jwtManager;

        public OrdersController(IOrderService orderService, IJwtManager jwtManager)
        {
            _orderService = orderService;
            _jwtManager = jwtManager;
        }

        [HttpGet("getall")]
        public  async Task<IActionResult> GetALl()
        {

            string userName = _jwtManager.GetUserNameByToken(Request.Headers["Authorization"].ToString().Split(" ")[1
                ]);
            return Ok(await _orderService.GetAllForUsersAsync(userName));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            string userName = _jwtManager.GetUserNameByToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
            return Ok(await _orderService.GetByIdAsync(id, userName));
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderPostDTO orderPostDTO)
        {
            await _orderService.PostAsync(orderPostDTO);
            return StatusCode(201);
        }
    }
}
