using AdvertisementService.Application.Features.PricingPlan.Command;
using AdvertisementService.Application.Features.PricingPlan.Queries;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Models.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AdvertisementService.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = AppRoles.Admin)]
    public class PricingPlansController : ApiControllers
    {

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<PricingPlan>>> GetAllPricingPlans()
        {
            var query = new GetAllPricingPlansQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PricingPlan>> GetPricingPlanById(Guid id)
        {
            var query = new GetPricingPlanByIdQuery().Id = id;
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<int>> CreatePricingPlan(CreatePricingPlanCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetPricingPlanById), new { id = result }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePricingPlan(Guid id, UpdatePricingPlanCommand command)
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
        public async Task<IActionResult> DeletePricingPlan(Guid id)
        {
            var command = new DeletePricingPlanCommand().Id = id;
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }
    }
}
