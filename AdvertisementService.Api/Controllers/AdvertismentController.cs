using AdvertisementService.Application.Common.Interfaces;
using AdvertisementService.Application.Features.Advertisement.Command;
using AdvertisementService.Application.Features.Advertisement.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Common.Errors;
using JobSeeker.Shared.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertismentController : ApiControllers
    {
        

        private readonly ILogger<AdvertismentController> _logger;
        private readonly IAdvertisementUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        //IMediator mediator
        public AdvertismentController(ILogger<AdvertismentController> logger, IAdvertisementUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _logger = logger;

        }


        [HttpGet("GetAdvertisements")]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAdvertisements()
        {
            var advertisements = await Mediator.Send(new GetAdvertisementsQuery());
            return Ok(advertisements);
        }

        [HttpGet("GetAdvertisement/{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(Guid id)
        {
            var advertisement = await Mediator.Send(new GetAdvertisementByIdQuery { Id = id });
            if (advertisement == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: ErrorMessages.NotFound);
                //return NotFound();
            }
            return Ok(advertisement);
        }

        [Authorize(Roles = AppRoles.Staff)]
        [HttpPost("AddAdvertisement")]
        public async Task<ActionResult> AddAdvertisement(CreateAdvertisementCommand command)
        {
            var id = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetAdvertisement), new { id }, command);
        }

        [Authorize(Roles = AppRoles.Combinations.StaffOrAdmin)]
        [HttpPut("UpdateAdvertisement/{id}")]
        public async Task<IActionResult> UpdateAdvertisement(Guid id, UpdateAdvertisementCommand command)
        {
            if (id != command.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ErrorMessages.BadRequest);
                //return BadRequest();
            }
            await Mediator.Send(command);
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Not Context");
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete("DeleteAdvertisement/{id}")]
        public async Task<IActionResult> DeleteAdvertisement(Guid id)
        {
            await Mediator.Send(new DeleteAdvertisementCommand { Id = id });
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "Not Context");
        }

        [HttpPut("approve/{id}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> ApproveAdvertisement(Guid id)
        {
            var advertisement = await _unitOfWork.AdvertisementRepository.GetByIdAsync(id);
            if (advertisement == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: ErrorMessages.NotFound);
                //return NotFound();
            }

            advertisement.IsApproved = true;
            await _unitOfWork.AdvertisementRepository.UpdateAsync(advertisement);
            await _unitOfWork.CommitAsync();
            return Ok($"Advertisement {id} approved.");
        }

        // Staff can list their own advertisements
        [HttpGet("staff/me")]
        [Authorize(Roles = AppRoles.Staff)]
        public async Task<IActionResult> GetMyAdvertisements()
        {
            // Assuming you have a way to get the current logged-in user's ID
            var currentUserId = _currentUserService.UserId;
            var advertisements = await _unitOfWork.AdvertisementRepository.FindAsync(a => a.UserId == currentUserId);
            return Ok(advertisements);
        }

        // Admin can list all advertisements or filter as needed
        [HttpGet("admin/all")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> GetAllAdvertisementsForAdmin()
        {
            var advertisements = await _unitOfWork.AdvertisementRepository.GetAllAsync();
            return Ok(advertisements);
        }
    }
}
