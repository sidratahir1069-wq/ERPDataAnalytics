using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceservice;

        public AttendanceController(IAttendanceService attendanceService)
        {

            _attendanceservice = attendanceService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await  _attendanceservice.GetAllAttendance();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await  _attendanceservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"attendanceID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Attendance dto) //ab create kro
        {
            var result = await  _attendanceservice.AddAttendance(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Attendance dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await  _attendanceservice.UpdateAttendance(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Attendance with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await  _attendanceservice.DeleteAttendance(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Attendance with ID {id} not found" });


            return NoContent();
        }
    }
}
    
