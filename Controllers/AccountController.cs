using Controle_de_Epis.Models.Identity;
using Controle_de_Epis.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Epis.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser>  _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
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

            var usuario = await _signInManager.UserManager
                .FindByEmailAsync(model.Email);

            if (usuario == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Email ou senha inválidos.");

                return View(model);
            }
            if (!usuario.Ativo)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Usuário inativo.");
                return View(model);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                usuario,
                model.Senha,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {

                await _signInManager.SignInAsync(
                    usuario,
                    model.LembrarMe);

                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Usuário bloqueado.");
            }
            else if (result.IsNotAllowed)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Usuário não autorizado a entrar.");
            }
            else
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Email ou senha inválidos.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
