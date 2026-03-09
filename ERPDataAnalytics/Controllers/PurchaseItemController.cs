using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseItemController : ControllerBase
    {
        private readonly IPurchaseItemService _PurchaseItemservice;

        public PurchaseItemController(IPurchaseItemService PurchaseItemService)
        {

            _PurchaseItemservice = PurchaseItemService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _PurchaseItemservice.GetAllPurchaseItem();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _PurchaseItemservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"PurchaseItemID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PurchaseItem dto) //ab create kro
        {
            var result = await _PurchaseItemservice.AddPurchaseItem(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PurchaseItem dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _PurchaseItemservice.UpdatePurchaseItem(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"PurchaseItem with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _PurchaseItemservice.DeletePurchaseItem(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"PurchaseItem with ID {id} not found" });


            return NoContent();
        }
    }
}
