using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PuzzleCircuit.API.Contracts;
using PuzzleCircuit.DAL.Entities.Admin;

namespace PuzzleCircuit.API.Controllers;

[ApiController]
[Route("user")]
public class UserController(UserManager<AppUser> userManager) : ControllerBase {

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<CurrentUserResponse>> Me() {
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

