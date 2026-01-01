using AdvertisementService.Application.Features.Feature.Command;
using AdvertisementService.Application.Features.Feature.Queries;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Contracts.Feature;
using JobSeeker.Shared.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementService.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = AppRoles.Admin)]
    public class FeaturesController : ApiController
    {
        [HttpGet("GetAllFeatures")]
        public async Task<ActionResult<List<Feature>>> GetAllFeatures()
        {
            var query = new GetAllFeaturesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("GetFeatures")]
        public async Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures()
        {
            var result = await Mediator.Send(new GetFeaturesQuery());
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Feature>> GetFeatureById(Guid id)
        {
            var query = new GetFeatureByIdQuery().Id = id;
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateFeature(CreateFeatureCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetFeatureById), new { id = result }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeature(Guid id, UpdateFeatureCommand command)
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
        public async Task<IActionResult> DeleteFeature(Guid id)
        {
            var command = new DeleteFeatureCommand().Id = id;
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }

}
