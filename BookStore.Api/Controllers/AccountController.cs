using BookStore.Api.Models;
using BookStore.Api.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepositry;

        public AccountController(IAccountRepository accountRepositry)
        {
            _accountRepositry = accountRepositry;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel signUpModel)
        {
            var result = await _accountRepositry.SignUpAsync(signUpModel);

            if(result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

    }
}
