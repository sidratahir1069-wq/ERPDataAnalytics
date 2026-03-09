using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _Saleservice;

        public SaleController(ISaleService SaleService)
        {

            _Saleservice = SaleService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Saleservice.GetAllSale();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Saleservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"SaleID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Sale dto) //ab create kro
        {
            var result = await _Saleservice.AddSale(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Sale dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Saleservice.UpdateSale(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Sale with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Saleservice.DeleteSale(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Sale with ID {id} not found" });


            return NoContent();
        }
    }
}
