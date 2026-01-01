using AdvertisementService.Application.Features.PricingFeature.Command;
using AdvertisementService.Application.Features.PricingFeature.Queries;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementService.Api.Controllers
{
    [Route("api/[controller]")]
    public class PricingFeaturesController : ApiController
    {

        public PricingFeaturesController()
        {
        }
        [HttpGet]
        [Authorize(Roles = AppRoles.Combinations.UserOrStaffOrAdmin)]
        public async Task<ActionResult<List<PricingFeature>>> GetAllPricingFeatures()
        {
            var query = new GetAllPricingFeaturesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = AppRoles.Combinations.UserOrStaffOrAdmin)]

        public async Task<ActionResult<PricingFeature>> GetPricingFeatureById(Guid id)
        {
            var query = new GetPricingFeatureByIdQuery().Id = id;
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]

        public async Task<ActionResult<int>> CreatePricingFeature(CreatePricingFeatureCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetPricingFeatureById), new { id = result }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = AppRoles.Admin)]

        public async Task<IActionResult> UpdatePricingFeature(Guid id, UpdatePricingFeatureCommand command)
        {
            if (id != command.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "BadRequest");
            }
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> DeletePricingFeature(Guid id)
        {
            var command = new DeletePricingFeatureCommand().Id = id;
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }
}
