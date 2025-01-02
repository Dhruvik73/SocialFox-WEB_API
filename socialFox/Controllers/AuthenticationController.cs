using Domains.ViewModels.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace socialFox.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _Service;
        private readonly IValidator<users> _validator;

        public AuthenticationController(IAuthenticationService Service, IValidator<users> validator)
        {
            _Service = Service;
            _validator = validator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel userDetails)
        {
            return Ok(await _Service.VerifyUser(userDetails?.UserEmail, userDetails?.Password));
        }
        [HttpPost("verifyToken")]
        public IActionResult VerifyToken(LoginModel newUser)
        {
            return Ok(_Service.VerifyToken(newUser.Token));
        }
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(users newUser)
        {
            var validationResult = _validator.Validate(newUser, options => { options.IncludeAllRuleSets(); });

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            return Ok(await _Service.CreateUser(newUser));
        }
    }
    public class LoginModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
