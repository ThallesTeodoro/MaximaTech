using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MaximaTech.Core.Entities;
using MaximaTech.Core.Interfaces.Repositories;
using MaximaTech.Domain.Commands.Requests;
using MaximaTech.Web.Extensions;
using MaximaTech.Web.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MaximaTech.Web.Controllers
{
    [Route("/login")]
    [AnonymousOnlyFilter("Index", "Dashboard")]
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="uow"></param>
        public LoginController(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        /// <summary>
        /// Show login form page
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Redirect To Result</returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Auth(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                User user = await _uow.UserRepository.FindByEmailAsync(request.Email);

                if (user == null || HashExtension.Validate(
                    request.Password,
                    _configuration["AUTH_SALT"],
                    user.Password) == false)
                {
                    ModelState.AddModelError("Email", "Invalid credentials");
                    return View("Index", request);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                await HttpContext.SignInAsync(
                    "MaximaTechScheme",
                    new ClaimsPrincipal(new ClaimsIdentity(claims, "MaximaTechScheme")),
                    new AuthenticationProperties
                    {
                        IsPersistent = request.RememberMe
                    }
                );

                return RedirectToAction("Index", "Dashboard");
            }

            return View("Index", request);
        }
    }
}