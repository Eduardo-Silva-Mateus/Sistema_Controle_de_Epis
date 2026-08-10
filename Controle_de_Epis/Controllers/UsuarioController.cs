using Controle_de_Epis.Models;
using Controle_de_Epis.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Controle_de_Epis.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarUsuario(UsuarioCreateViewModel usuario)
        {
            if(!ModelState.IsValid)
            {
                return View(usuario);
            }

            var result = await _usuarioService.CreateUsuarioAsync(usuario);

            if(result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(usuario);
        }
    }
}