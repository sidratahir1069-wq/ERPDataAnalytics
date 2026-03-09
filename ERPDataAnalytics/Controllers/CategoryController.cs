using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       private readonly ICategoryService _Categoryservice;

        public CategoryController(ICategoryService CategoryService)
        {

            _Categoryservice = CategoryService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Categoryservice.GetAllCategory();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Categoryservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"CategoryID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Category dto) 
        {
            var result = await _Categoryservice.AddCategory(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Categoryservice.UpdateCategory(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Category with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Categoryservice.DeleteCategory(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Category with ID {id} not found" });


            return NoContent();
        }
    }
}
