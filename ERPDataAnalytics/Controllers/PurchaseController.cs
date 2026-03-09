using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _Purchaseservice;

        public PurchaseController(IPurchaseService PurchaseService)
        {

            _Purchaseservice = PurchaseService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Purchaseservice.GetAllPurchase();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Purchaseservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"PurchaseID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Purchase dto) //ab create kro
        {
            var result = await _Purchaseservice.AddPurchase(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Purchase dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Purchaseservice.UpdatePurchase(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Purchase with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Purchaseservice.DeletePurchase(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Purchase with ID {id} not found" });


            return NoContent();
        }
    }
}
