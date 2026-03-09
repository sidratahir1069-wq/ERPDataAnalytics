using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _Payrollservice;

        public PayrollController(IPayrollService PayrollService)
        {

            _Payrollservice = PayrollService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Payrollservice.GetAllPayroll();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Payrollservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"PayrollID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Payroll dto) //ab create kro
        {
            var result = await _Payrollservice.AddPayroll(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Payroll dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Payrollservice.UpdatePayroll(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Payroll with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Payrollservice.DeletePayroll(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Payroll with ID {id} not found" });


            return NoContent();
        }
    }
}
