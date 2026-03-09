using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleItemController : ControllerBase
    {
        private readonly ISaleItemService _SaleItemservice;

        public SaleItemController(ISaleItemService SaleItemService)
        {

            _SaleItemservice = SaleItemService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _SaleItemservice.GetAllSaleItem();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _SaleItemservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"SaleItemID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SaleItem dto) //ab create kro
        {
            var result = await _SaleItemservice.AddSaleItem(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaleItem dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _SaleItemservice.UpdateSaleItem(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"SaleItem with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _SaleItemservice.DeleteSaleItem(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"SaleItem with ID {id} not found" });


            return NoContent();
        }
    }
}
