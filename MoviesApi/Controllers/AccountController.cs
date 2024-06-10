using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(dtoNewUser dtouser)
        {
            if (ModelState.IsValid)
            {
                //map dto with appuser
                AppUser appUser = new()
                {
                    UserName = dtouser.userName,
                    Email = dtouser.email,
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, dtouser.password);
                if (result.Succeeded)
                {
                    return Ok("Success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(dtoLogin dtolog)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByNameAsync(dtolog.userName);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, dtolog.password))
                    {
                        return Ok("Token");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Name is invalid");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
