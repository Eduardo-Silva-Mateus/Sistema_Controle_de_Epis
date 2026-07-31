using Controle_de_Epis.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Controle_de_Epis.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser>  _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() 
        {
             return View();
        }

    }
}
