using ERPDataAnalytics.Application.cs.Interface;
using ERPDataAnalytics.domain.cs.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERPDataAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _Companyservice;

        public CompanyController(ICompanyService CompanyService)
        {

            _Companyservice = CompanyService;

        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Companyservice.GetAllCompany();
            return Ok(result);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _Companyservice.GetById(id);
            if (result == null)
                return NotFound(new { Message = $"CompanyID {id} not found" });

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Company dto) //ab create kro
        {
            var result = await _Companyservice.AddCompany(dto);
            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Company dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Companyservice.UpdateCompany(id, dto);

            if (result == null || !result.Success)
                return NotFound(new { Message = $"Company with ID {id} not found" });

            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _Companyservice.DeleteCompany(id);

            if (result == null || !result.Data)
                return NotFound(new { Message = $"Company with ID {id} not found" });


            return NoContent();
        }
    }
}
