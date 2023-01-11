using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCoreLogin.Controllers
{
    [Authorize(Roles ="admin")]//rolü admin olan girebilsin admin indexe diye
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
