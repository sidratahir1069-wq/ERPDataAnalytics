using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _Shiftservice;

        public ShiftController(IShiftService ShiftService)
        {

            _Shiftservice = ShiftService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Shiftservice.GetAllShift();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Shiftservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"ShiftID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Shift dto) //ab create kro
        {
            var result = await _Shiftservice.AddShift(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Shift dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Shiftservice.UpdateShift(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Shift with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Shiftservice.DeleteShift(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Shift with ID {id} not found" });


            return NoContent();
        }
    }
}
