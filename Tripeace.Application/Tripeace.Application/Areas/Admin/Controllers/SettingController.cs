using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tripeace.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Roles = "GameMaster, God")]
    public class SettingController : Controller
    {
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public IActionResult AdminSettings()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "GameMaster, God")]
        public IActionResult WebsiteSettings()
        {
            return View();
        }
    }
}
