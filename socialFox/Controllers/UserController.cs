using Domains.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Services.Interfaces;

namespace socialFox.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<users> service;
        private readonly IUserServices userServices;
        public UserController(IService<users> service,IUserServices userServices)
        {
            this.service = service;
            this.userServices = userServices;
        }
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await service.GetAll());
        }

        [HttpPost("getLogedUserDetails/{id}")]
        public async Task<IActionResult> GetLogedUserDetails([FromRoute]string id)
        {
            return Ok(await userServices.GetLogedUserDetails(id));
        }
        [HttpPost("getSuggestedAllies/{id}")]
        public IActionResult GetSuggestedAllies([FromRoute] string id)
        {
            return Ok(userServices.GetSuggestedAllies(id));
        }
    }
}
