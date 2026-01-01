using AdvertisementService.Application.Features.PricingCategory.Command;
using AdvertisementService.Application.Features.PricingCategory.Queries;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Common.Errors;
using JobSeeker.Shared.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PricingCategoryController : ApiControllers
    {
        /// <summary>
        /// Get all pricing categories
        /// </summary>
        [HttpGet]
        [Authorize(Roles = AppRoles.Combinations.StaffOrAdmin)]
        public async Task<ActionResult<List<PricingCategory>>> GetPricingCategories()
        {
            var query = new GetAllPricingCategoriesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get a specific pricing category by ID
        /// </summary>
        /// <param name="id">The ID of the pricing category</param>
        [HttpGet("{id}")]
        [Authorize(Roles = AppRoles.Combinations.UserOrStaffOrAdmin)]

        public async Task<ActionResult<PricingCategory>> GetPricingCategory(int id)
        {
            var query = new GetPricingCategoryByIdQuery().Id = id;
            var result = await Mediator.Send(query);

            if (result == null)
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: ErrorMessages.NotFound);

            return Ok(result);
        }

        /// <summary>
        /// Create a new pricing category
        /// </summary>
        [HttpPost]
        [Authorize(Roles = AppRoles.Admin)]

        public async Task<ActionResult<int>> CreatePricingCategory(CreatePricingCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetPricingCategory), new { id = result }, result);
        }

        /// <summary>
        /// Update an existing pricing category
        /// </summary>
        /// <param name="id">The ID of the pricing category to update</param>
        [HttpPut("{id}")]
        [Authorize(Roles = AppRoles.Admin)]

        public async Task<IActionResult> UpdatePricingCategory(int id, UpdatePricingCategoryCommand command)
        {
            if (id != command.Id)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ErrorMessages.IdMissMatch);

            //return BadRequest("ID in the URL does not match the ID in the request body");

            var result = await Mediator.Send(command);

            if (result == null)
                //return NotFound();
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: ErrorMessages.NotFound);

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        /// <summary>
        /// Delete a pricing category
        /// </summary>
        /// <param name="id">The ID of the pricing category to delete</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> DeletePricingCategory(int id)
        {
            var command = new DeletePricingCategoryCommand().Id = id;
            var result = await Mediator.Send(command);

            if (result == null)
                //    return NotFound();
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: ErrorMessages.NotFound);

            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }
}
