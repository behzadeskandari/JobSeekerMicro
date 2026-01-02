using AdvertisementService.Application.Common.Interfaces;
using AdvertisementService.Application.Features.CustomerAddress.Command;
using AdvertisementService.Application.Features.CustomerAddress.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementService.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerAddressesController : ApiControllers
    {
        private readonly ILogger<CustomerAddressesController> _logger;
        private readonly IAdvertisementUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public CustomerAddressesController(ILogger<CustomerAddressesController> logger, IAdvertisementUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _logger = logger;

        }

        [HttpGet("GetCustomerAddresses")]
        public async Task<IActionResult> GetCustomerAddresses()
        {
            var customerAddresses = await _unitOfWork.CustomerAddressRepository.GetAllAsync();
            return Ok(customerAddresses);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> GetCustomerAddressById(Guid id)
        {
            var query = new GetCustomerAddressByIdQuery().Id = id;
            var result = await Mediator.Send(query);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomerAddress(CreateCustomerAddressCommand command)
        {
            var result = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomerAddressById), new { id = result }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAddress(Guid id, UpdateCustomerAddressCommand command)
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
        public async Task<IActionResult> DeleteCustomerAddress(Guid id)
        {
            var command = new DeleteCustomerAddressCommand().Id = id;
            var result = await Mediator.Send(command);
            if (result == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

    }
}
