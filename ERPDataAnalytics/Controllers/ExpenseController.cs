using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _Expenseservice;

        public ExpenseController(IExpenseService ExpenseService)
        {

            _Expenseservice =ExpenseService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Expenseservice.GetAllExpense();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Expenseservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"ExpenseID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]Expense dto) //ab create kro
        {
            var result = await _Expenseservice.AddExpense(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]Expense dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Expenseservice.UpdateExpense(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Expense with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Expenseservice.DeleteExpense(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Expense with ID {id} not found" });


            return NoContent();
        }
    }
}
