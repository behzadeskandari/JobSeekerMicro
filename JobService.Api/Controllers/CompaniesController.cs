using JobService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IJobUnitOfWork _unitOfWork;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(IJobUnitOfWork unitOfWork, ILogger<CompaniesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Gets the owner userId for a company
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns>Company owner userId or 404 if not found</returns>
        [HttpGet("{id}/owner")]
        [ProducesResponseType(typeof(CompanyOwnerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyOwner(Guid id)
        {
            var company = await _unitOfWork.Company.GetByIdAsync(id);
            
            if (company == null)
            {
                return NotFound();
            }
            
            return Ok(new CompanyOwnerResponse { UserId = company.UserId });
        }
    }

    public class CompanyOwnerResponse
    {
        public string UserId { get; set; } = string.Empty;
    }
}

