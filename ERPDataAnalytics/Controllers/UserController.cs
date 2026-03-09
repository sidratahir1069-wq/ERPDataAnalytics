using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _Userservice;

        public UserController(IUserService UserService)
        {

            _Userservice = UserService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Userservice.GetAllUser();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Userservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"UserID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] User dto) //ab create kro
        {
            var result = await _Userservice.AddUser(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] User dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Userservice.UpdateUser(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"User with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Userservice.DeleteUser(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"User with ID {id} not found" });


            return NoContent();
        }
    }
}
