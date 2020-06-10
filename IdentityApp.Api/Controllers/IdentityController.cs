using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        // GET
        public IActionResult Get()
        {
            var output = new JsonResult(User.Claims.Select(c => new {c.Type, c.Value}));
            return output;
        }
    }
}