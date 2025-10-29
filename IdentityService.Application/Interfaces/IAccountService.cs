using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using IdentityService.Domain.Entities;
using JobSeeker.Shared.Contracts.User;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.Interfaces
{
    public interface IAccountService
    {

        Task<UserDto> CraeteApplicationUserDto(User user);
        Task<bool> CheckEmailExistsAsync(string email);

        Task<User> FindByNameAsync(string UserName);

        Task<bool> CheckPasswordSignInAsync(User user, string Password, bool lockout);
        Microsoft.AspNetCore.Mvc.ActionResult<UserDto> CreateApplicationUserDto(User user);
        Task<Result<User>> CreateUserAsync(User userToAdd, string password);

        Task<SignInResult> SignInUserAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task<User?> FindByEmailAsync(string payloadEmail);

        Task<Result<User>> CreateUserAsync(User userToAdd);
        Task SignInUserAsync(string userName, string password);

        Task<User> AddRoleAsync(User user, string role);
        Task<User> FindByIdAsync(string userId);

        Task<User> SignOutUserAsync(User user, string role);

        Task<(bool Succeeded, string UserId, IEnumerable<string> Errors)> CreateUserAsync(string userName, string email, string password);
        Task<(bool Succeeded, string UserId, IEnumerable<string> Errors)> CreateUserAsync(string userName, string email, string password, string firstName, string lastName);
        Task<bool> UserExistsAsync(string userId);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> IsUserInRoleAsync(string userId, string role);
        Task<bool> AddUserToRoleAsync(string userId, string role);
        Task<bool> RemoveUserFromRoleAsync(string userId, string role);
        Task<IList<string>> GetUserRolesAsync(string userId);

        // Authentication
        Task<bool> CheckPasswordAsync(string userId, string password);
        Task<(bool Succeeded, string UserId, IEnumerable<string> Errors)> SignInAsync(string email);
        Task SignOutAsync();
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<bool> ConfirmEmailAsync(string userId, string token);

        // Password management
        Task<string> GeneratePasswordResetTokenAsync(string userId);
        Task<bool> ResetPasswordAsync(string userId, string token, string newPassword);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        // Claims
        Task<IList<Claim>> GetUserClaimsAsync(string userId);
        Task<bool> AddClaimAsync(string userId, Claim claim);
        Task<bool> RemoveClaimAsync(string userId, Claim claim);

        Task<User?> GetUserAsync(ClaimsPrincipal claimsPrincipal);

        Task<string> GenerateOtp(int length = 8);
    }

}
