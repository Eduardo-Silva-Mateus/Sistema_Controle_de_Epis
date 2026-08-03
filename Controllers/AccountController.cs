using Controle_de_Epis.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Controle_de_Epis.ViewModel;
using Microsoft.AspNetCore.Authorization;
namespace Controle_de_Epis.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

if (user == null)
{
    ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
    return View(model);
}

var result = await _signInManager.PasswordSignInAsync(
    user.UserName!,
    model.Senha,
    model.LembrarMe,
    lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Conta bloqueada.");
            }
            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "Login não permitido.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
