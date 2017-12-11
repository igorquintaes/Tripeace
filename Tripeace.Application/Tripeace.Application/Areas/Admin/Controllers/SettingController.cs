using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tripeace.Application.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        [Area("Admin")]
        [Route("admin/[controller]")]
        [Authorize(Roles = "GameMaster, God")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
