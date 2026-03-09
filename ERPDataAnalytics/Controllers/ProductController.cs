using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _Productservice;

        public ProductController(IProductService ProductService)
        {

            _Productservice = ProductService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Productservice.GetAllProduct();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Productservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"ProductID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Product dto) //ab create kro
        {
            var result = await _Productservice.AddProduct(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Productservice.UpdateProduct(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Product with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Productservice.DeleteProduct(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Product with ID {id} not found" });


            return NoContent();
        }
    }
}
