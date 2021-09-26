using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaximaTech.Web.Controllers
{
    [Route("")]
    [Authorize(AuthenticationSchemes = "MaximaTechScheme")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Dashboard view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Login");
        }
    }
}