using FluentResults;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IAccountService accountService, ILogger<UsersController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Checks if a user exists by userId
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>200 if exists, 404 if not found</returns>
        [HttpGet("{id}/exists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UserExists(string id)
        {
            var exists = await _accountService.UserExistsAsync(id);
            
            if (exists)
            {
                return Ok();
            }
            
            return NotFound();
        }
    }
}

