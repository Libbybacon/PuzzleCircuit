using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PuzzleCircuit.API.Contracts;
using PuzzleCircuit.DAL.Entities.Admin;

namespace PuzzleCircuit.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    SignInManager<AppUser> signInManager,
    UserManager<AppUser> userManager,
    ILogger<AuthController> logger) : ControllerBase {

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request) {

        AppUser? existing = await userManager.FindByEmailAsync(request.Email);
        if (existing is not null) {
            return BadRequest(ErrorCodes.EMAIL_IN_USE);
        }

        AppUser user = new() {
            UserName = request.Email,
            Email = request.Email,
            DisplayName = request.DisplayName,
            Location = request.Location,
            IsActive = true
        };

        IdentityResult result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded) {
            return BadRequest(result.Errors.Select(e => new {
                e.Code,
                e.Description
            }));
        }

        await signInManager.SignInAsync(user, isPersistent: true);

        return Ok(new {
            user.Id,
            user.Email
        });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request) {
        AppUser? existing = await userManager.FindByEmailAsync(request.Email);
        if (existing is null) {
            return BadRequest(ErrorCodes.EMAIL_NOT_IN_USE);
        }

        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(
            request.Email,
            request.Password,
            isPersistent: true,
            lockoutOnFailure: false);

        return !result.Succeeded ? Unauthorized() : Ok();
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout() {
        await signInManager.SignOutAsync();

        return Ok(new {
            reason = "LOGGED_OUT",
            message = "Logout successful."
        });
    }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request) {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        // Do not reveal whether the email exists.
        if (user is null) {
            return Ok();
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);

        logger.LogInformation(
            "Password reset token for {Email}: {Token}",
            request.Email,
            token);

        // TODO: email this token/link instead of logging it.

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request) {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);

        if (user is null) {
            return BadRequest(ErrorCodes.EMAIL_NOT_IN_USE);
        }

        IdentityResult result = await userManager.ResetPasswordAsync(
            user,
            request.ResetCode,
            request.NewPassword);

        return !result.Succeeded
            ? BadRequest(result.Errors.Select(e => new {
                e.Code,
                e.Description
            }))
            : Ok();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<CurrentUserResponse>> Me(
        [FromServices] UserManager<AppUser> userManager) {
        AppUser? user = await userManager.GetUserAsync(User);
        return user is null
            ? (ActionResult<CurrentUserResponse>)Unauthorized(new {
                reason = "NOT_AUTHENTICATED",
                message = "No authenticated user."
            })
            : (ActionResult<CurrentUserResponse>)Ok(new CurrentUserResponse {
                Id = user.Id,
                Email = user.Email!,
                DisplayName = user.DisplayName
            });
    }
}

