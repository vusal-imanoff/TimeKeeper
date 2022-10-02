using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeKeeperFinal.Service.DTOs.AddressInformationDTO;
using TimeKeeperFinal.Service.Interfaces;
using TimeKeeperFinal.Service.JwtManager.Interfaces;

namespace TimeKeeperFinal.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressInformationService _addressInformationService;
        private readonly IJwtManager _jwtManager;

        public AddressesController(IAddressInformationService addressInformationService, IJwtManager jwtManager)
        {
            _addressInformationService = addressInformationService;
            _jwtManager = jwtManager;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            string userName = _jwtManager.GetUserNameByToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
            return Ok(await _addressInformationService.GetAllForUsersAsync(userName));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            string userName = _jwtManager.GetUserNameByToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
            return Ok(await _addressInformationService.GetByIdAsync(id, userName));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddressInformationPostDTO addressInformationPostDTO)
            {
            await _addressInformationService.PostAsync(addressInformationPostDTO);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id,AddressInformationPutDTO addressInformationPutDTO)
        {
            await _addressInformationService.PutAsync(id,addressInformationPutDTO);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _addressInformationService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _addressInformationService.RestoreAsync(id);

            return NoContent();
        }
    }
}
