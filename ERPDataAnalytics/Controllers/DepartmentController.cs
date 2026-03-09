using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _Departmentservice;

        public DepartmentController(IDepartmentService DepartmentService)
        {

            _Departmentservice = DepartmentService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Departmentservice.GetAllDepartment();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Departmentservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"DepartmentID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Department dto) //ab create kro
        {
            var result = await _Departmentservice.AddDepartment(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Department dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Departmentservice.UpdateDepartment(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Department with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Departmentservice.DeleteDepartment(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Department with ID {id} not found" });


            return NoContent();
        }
    }
}
