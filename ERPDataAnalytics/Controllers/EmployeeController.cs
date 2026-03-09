using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _Employeeservice;

        public EmployeeController(IEmployeeService EmployeeService)
        {

            _Employeeservice = EmployeeService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Employeeservice.GetAllEmployee();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Employeeservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"EmployeeID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Employee dto) //ab create kro
        {
            var result = await _Employeeservice.AddEmployee(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Employeeservice.UpdateEmployee(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Employee with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Employeeservice.DeleteEmployee(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Employee with ID {id} not found" });


            return NoContent();
        }
    }
}
