using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Trendlink.Api.Controllers
{
    public class FallbackController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.PhysicalFile(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"),
                "text/HTML"
            );
        }
    }
}
