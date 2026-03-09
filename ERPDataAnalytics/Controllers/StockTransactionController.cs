using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionController : ControllerBase
    {
        private readonly IStockTransactionService _StockTransactionservice;

        public StockTransactionController(IStockTransactionService StockTransactionService)
        {

            _StockTransactionservice = StockTransactionService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _StockTransactionservice.GetAllStockTransaction();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _StockTransactionservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"StockTransactionID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] StockTransaction dto) //ab create kro
        {
            var result = await _StockTransactionservice.AddStockTransaction(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] StockTransaction dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _StockTransactionservice.UpdateStockTransaction(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"StockTransaction with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _StockTransactionservice.DeleteStockTransaction(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"StockTransaction with ID {id} not found" });


            return NoContent();
        }
    }
}
