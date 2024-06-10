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
        [HttpPost]
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
    }
}
