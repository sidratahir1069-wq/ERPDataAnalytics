using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _Customerservice;

        public CustomerController(ICustomerService CustomerService)
        {

            _Customerservice = CustomerService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Customerservice.GetAllCustomer();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Customerservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"CustomerID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Customer dto) //ab create kro
        {
            var result = await _Customerservice.AddCustomer(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Customerservice.UpdateCustomer(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Customer with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Customerservice.DeleteCustomer(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Customer with ID {id} not found" });


            return NoContent();
        }
    }
}
