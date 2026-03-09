using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _Vendorservice;

        public VendorController(IVendorService VendorService)
        {

            _Vendorservice = VendorService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Vendorservice.GetAllVendor();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Vendorservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"VendorID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Vendor dto) //ab create kro
        {
            var result = await _Vendorservice.AddVendor(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Vendor dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Vendorservice.UpdateVendor(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Vendor with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Vendorservice.DeleteVendor(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Vendor with ID {id} not found" });


            return NoContent();
        }
    }
}
