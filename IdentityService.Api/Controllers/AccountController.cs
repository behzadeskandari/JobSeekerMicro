using System;
using System.Security.Claims;
using System.Text.Json;
using AppJob.Core.Services;
using Google.Apis.Auth;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Domain.IntegrationEventSourcing;
using JobSeeker.Shared.Contracts.Integration;
using IdentityService.Domain.Roles;
using IdentityService.Infrastructure.Jwt;
using JobSeeker.Shared.Common.Errors;
using JobSeeker.Shared.Contracts.Authentication;
using JobSeeker.Shared.Contracts.Response;
using JobSeeker.Shared.Contracts.Routes;
using JobSeeker.Shared.Contracts.User;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AccountController : ApiController
    {
        private readonly IJwtTokenGenerator _jwt;
        private readonly IAccountService _accountService;
        private readonly IIdentityUnitOfWOrk _unitOfWork;
        private readonly ICommunicationOrchestrator _communicationOrchestrator;//IEmailService _emailService;
        private readonly IPublishEndpoint _publishEndpoint;
        /// <summary>
        /// goes into the service not needed any more here 
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService, IJwtTokenGenerator jwtTokenGenerator,
            IPublishEndpoint publishEndpoint,
           // IEmailService emailService            
           ICommunicationOrchestrator CommunicationOrchestrator,
           IIdentityUnitOfWOrk unitOfWork
            )
        {

            _jwt = jwtTokenGenerator;
            _accountService = accountService;
            //_emailService = emailService;
            _communicationOrchestrator = CommunicationOrchestrator;
            _publishEndpoint = publishEndpoint;
            //    _unitOfWork = unitOfWork;
        }
        //"User,Staff"
        [Authorize(Roles = AppRoles.Combinations.UserOrStaff)]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await _accountService.FindByNameAsync(User.FindFirst(ClaimTypes.Name)?.Value);
            var userDto = _accountService.CreateApplicationUserDto(user);
            userDto.Value.JWT = await _jwt.GetToken(user);
            userDto.Value.RefreshToken = await _jwt.GenerateRefreshToken();
            userDto.Value.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            return userDto;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginModel)
        {
            // Try to find user by username first, then by email
            User user = await _accountService.FindByNameAsync(loginModel.UserName);
            if (user == null)
            {
                user = await _accountService.FindByEmailAsync(loginModel.UserName);
            }

            if (user == null) return Unauthorized(ErrorMessages.InvalidUser);

            var results = await _accountService.CheckPasswordAsync(user.Id, loginModel.Password);
            if (!results) return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: ErrorMessages.InvalidPassword);

            // Get user's existing roles
            var userRoles = await _accountService.GetUserRolesAsync(user.Id);
            string roleToAssign;
            string redirectUrl = string.Empty;

            // Determine role based on existing roles
            if (userRoles.Contains(AppRoles.Admin))
            {
                roleToAssign = AppRoles.Admin;
                redirectUrl = Routes.AdminDashBoard;
            }
            else if (userRoles.Contains(AppRoles.Staff))
            {
                roleToAssign = AppRoles.Staff;
                redirectUrl = Routes.StaffDashBoard;
            }
            else if (userRoles.Contains(AppRoles.User))
            {
                roleToAssign = AppRoles.User;
            }
            else
            {
                // Default to User role if no role is assigned
                roleToAssign = AppRoles.User;
            }

            var userWithRole = await _accountService.AddRoleAsync(user, roleToAssign);
            userWithRole.RedirectUrl = redirectUrl;

            var userDto = _accountService.CreateApplicationUserDto(userWithRole);
            userDto.Value.JWT = await _jwt.GetToken(userWithRole);
            userDto.Value.RefreshToken = await _jwt.GenerateRefreshToken();
            userDto.Value.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
            return Ok(new { message = SuccessMessages.LoginSuccess, Items = userDto });
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, [FromQuery] string role = AppRoles.User)
        {
            // Validate role
            if (role != AppRoles.Admin && role != AppRoles.Staff && role != AppRoles.User)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid role specified");
            }

            if (await _accountService.CheckEmailExistsAsync(registerDto.Email))
            {
                var user = await _accountService.GetUserByEmailAsync(registerDto.Email);
                var existingRoles = await _accountService.GetUserRolesAsync(user.Id);
                var Message = ErrorMessages.DuplicateEmail;

                if (existingRoles != null && user != null)
                {
                    if (registerDto.Email == user.Email && existingRoles.Contains(AppRoles.Staff))
                    {
                        return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "You Already Registered as A Staff");
                    }
                }
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: Message);
            }

            var userToAdd = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Password = registerDto.Password,
                PictureUrl = string.Empty,
                EmailConfirmed = true,
                Role = role
            };

            var result = await _accountService.CreateUserAsync(userToAdd, userToAdd.Password);

            if (!result.IsSuccess) return Problem(statusCode: StatusCodes.Status400BadRequest, detail: result.Errors.First().Message);

            var userRecord = await _accountService.AddRoleAsync(result.Value, role);

            // Publish UserRegisteredIntegrationEvent for all roles
            var userRegisteredEvent = new UserRegisteredIntegrationEvent(
                userRecord.Id,
                userRecord.Email,
                role,
                userRecord.FirstName,
                userRecord.LastName
            );

            await _unitOfWork.OutboxMessage.AddAsync(new OutboxMessage
            {
                Id = userRegisteredEvent.Id,
                Type = nameof(UserRegisteredIntegrationEvent),
                Content = JsonSerializer.Serialize(userRegisteredEvent),
                OccurredOn = DateTime.UtcNow
            });

            await _unitOfWork.CommitAsync();

            await _publishEndpoint.Publish(userRegisteredEvent);

            return Ok(new { message = SuccessMessages.AccountCreated, Items = userRecord });
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
        {
            var userRecord = new User();
            var userDto = new UserDto();
            var token = string.Empty;
            var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.IdToken);
            if (payload == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid Google token"); //BadRequest("Invalid Google token.");
            }
            var user = await _accountService.FindByEmailAsync(payload.Email);
            if (user == null)
            {


                user = new User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    PictureUrl = payload.Picture,
                    EmailConfirmed = true,
                    Password = await _accountService.GenerateOtp(6) + "!@",
                };
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password: user.Password);


                var createResult = await _accountService.CreateUserAsync(user);
                if (!createResult.IsSuccess)
                    return BadRequest(createResult.Errors);


                await _accountService.AddRoleAsync(user, "User");

                //string body = $"ورود کاربر : {user.Email} با پسورد {user.Password}";
                //var results = await _communicationOrchestrator.SendEmailAsync(
                //    to: user.Email,
                //    subject: "کاربر جدید",
                //    body: body);
            }
            token = await _jwt.GenerateToken(user);

            userDto = new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PictureUrl = user.PictureUrl,
                JWT = token,
                Role = (await _accountService.GetUserRolesAsync(user.Id)).FirstOrDefault(),
                EmailConfirmed = user.EmailConfirmed
            };

            return Ok(new { message = "Google login successful", Items = userDto });
        }

        [HttpGet("linkedin-login")]
        public IActionResult LinkedInLogin(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("LinkedInCallback", new { returnUrl }) };
            return Challenge(properties, "LinkedIn");
        }

        [HttpGet("linkedin-callback")]
        public async Task<IActionResult> LinkedInCallback(string returnUrl = "/")
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: authenticateResult.Failure.Message); // Or redirect to error page

            // Access user info from authenticateResult.Principal
            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            // Extract email, name, etc. from claims
            // Do your user registration/login logic here

            return Redirect(returnUrl);
        }
        //"User,Staff"
        [Authorize(Roles = AppRoles.Combinations.UserOrStaff)] // Requires a valid JWT token
        [HttpGet("check-login")]
        public async Task<IActionResult> CheckLogin()
        {
            // Get the user ID from the JWT token (stored in the User property)
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine($"User.Identity.IsAuthenticated: {User.Identity.IsAuthenticated}");
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim: {claim.Type} => {claim.Value}");
            }

            if (email == null)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, detail: "No valid user found in token");//Unauthorized(new { IsLoggedIn = false, Message = "No valid user found in token" });
            }

            // Fetch the user from Identity
            var user = await _accountService.FindByEmailAsync(email);
            if (user == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: ErrorMessages.UserNotFound);//NotFound();
            }
            // Return success response with user details
            return Ok(new
            {
                IsLoggedIn = true,
                Email = user.UserName,
                Role = user.Role
            });
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> LogOut(User user, string role = "User")
        {
            var userRecord = _accountService.SignOutUserAsync(user, role);

            if (userRecord.Result is not null)
            {
                return Ok(new { message = SuccessMessages.LogOutSucess, Items = user });
            }

            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: ErrorMessages.ErrorInLogout);//BadRequest(new { message = ErrorMessages.ErrorInLogout });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!await _accountService.UserExistsByEmailAsync(request.Email))
            {
                // Don't reveal that the user does not exist
                return Ok();
            }

            var user = await _accountService.GetUserByEmailAsync(request.Email);
            var token = await _accountService.GeneratePasswordResetTokenAsync(user.Id);

            // In a real application, you would send an email with the token
            // For demo purposes, we'll just return it
            return Ok(new { token });
        }

        [HttpPost("getUserStatus")]
        public async Task<IActionResult> GetUserStatus([FromBody] JwtToken token)
        {
            //var tok = token as JwtToken;

            var responseToken = _jwt.ReadToken(token.token);

            //if (responseToken != null && responseToken.UserId != null)
            //{
            //    bool hasActiveCompany = false;
            //    var isSignedIn = true;
            //    var findedCompany = await _unitOfWork.companyRepository.FindAsync(x => x.UserId == responseToken.UserId);
            //    if (findedCompany.Count() > 0)
            //    {
            //        hasActiveCompany = true;
            //    }

            //    return Ok(new { token, isSignedIn, hasActiveCompany });
            //}
            //else
            //{
            //    var isSignedIn = false;
            //    var hasActiveCompany = false;
            //    return Ok(new { token, isSignedIn, hasActiveCompany });
            //}
            throw new NotImplementedException();
            //:TODO
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] JobSeeker.Shared.Contracts.Authentication.ResetPasswordRequest request)
        {
            var user = await _accountService.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Ok();
            }
            var result = await _accountService.ResetPasswordAsync(
                user.Id,
                request.Token,
                request.NewPassword);

            if (result)
            {
                return Ok();
            }

            return BadRequest(new { error = "Failed to reset password" });
        }
   
    }

}
