using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _Branchservice;

        public BranchController(IBranchService BranchService)
        {

            _Branchservice = BranchService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Branchservice.GetAllBranch();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Branchservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"BranchID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Branch dto) 
        {
            var result = await _Branchservice.AddBranch(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Branch dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Branchservice.UpdateBranch(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Branch with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Branchservice.DeleteBranch(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Branch with ID {id} not found" });


            return NoContent();
        }
    }
}
