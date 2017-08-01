using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripeace.Application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    [Authorize(Roles = "GameMaster, God")]
    public class DashboardController : Controller
    {
        public DashboardController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
