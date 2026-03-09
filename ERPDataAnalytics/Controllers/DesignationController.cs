using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _Designationservice;

        public DesignationController(IDesignationService DesignationService)
        {

            _Designationservice = DesignationService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Designationservice.GetAllDesignation();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Designationservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"DesignationID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Designation dto) //ab create kro
        {
            var result = await _Designationservice.AddDesignation(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Designation dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Designationservice.UpdateDesignation(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Designation with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Designationservice.DeleteDesignation(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Designation with ID {id} not found" });


            return NoContent();
        }
    }
}
