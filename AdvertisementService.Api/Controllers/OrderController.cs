using AdvertisementService.Application.Features.Order.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Contracts.Order;
using JobSeeker.Shared.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementService.Api.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    [Route("api/[controller]")]
    public class OrderController : ApiController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IAdvertisementUnitOfWork _unitOfWork;
        public OrderController(
          ILogger<OrderController> logger,
          IAdvertisementUnitOfWork advertisementUnitOfWork
          )
        {
            _logger = logger;
            _unitOfWork = advertisementUnitOfWork;
        }


        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _unitOfWork.OrdersRepository.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(id);
            if (order == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _unitOfWork.OrdersRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] Order order)
        {
            if (!ModelState.IsValid || id != order.Id)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ModelState.ToString());
            }

            var existingOrder = await _unitOfWork.OrdersRepository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.OrdersRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _unitOfWork.OrdersRepository.GetByIdAsync(id);
            if (order == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "NotFound");
            }

            await _unitOfWork.OrdersRepository.DeleteAsync(order);
            await _unitOfWork.CommitAsync();
            return Problem(statusCode: StatusCodes.Status204NoContent, detail: "NoContent");
        }

        [HttpGet("GetUserOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new GetUserOrdersQuery { UserId = userId });
            return result.IsSuccess ? Ok(result.Value) :
                Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Reasons.ToString());
            //BadRequest(result.Reasons);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await Mediator.Send(new GetOrderByIdQuery { Id = id });
            return result != null ? Ok(result) : Problem(statusCode: StatusCodes.Status404NotFound, detail: result.ToString());
        }


    }
}
