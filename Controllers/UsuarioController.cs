using Controle_de_Epis.Enums;
using Controle_de_Epis.Infrastructure.Identity;
using Controle_de_Epis.Models;
using Controle_de_Epis.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Controle_de_Epis.Controllers
{
    [Authorize(Roles = Roles.Admin)]
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
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var result = await _usuarioService.CreateUsuarioAsync(usuario);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuario(string id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(UsuarioEditViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var result = await _usuarioService.UpdateUsuarioAsync(usuario);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarStatus(string id, bool novoStatus)
        {
            TempData["Teste"] = $"ID: {id} | Novo Status: {novoStatus}";

            var result = await _usuarioService.AlterarStatusAsync(id, novoStatus);

            if (!result)
            {
                TempData["Erro"] = "Não foi possível alterar o status do usuario";
                return RedirectToAction(nameof(Index));
            }

            TempData["Sucesso"] = "Status do usuario alterado com sucesso";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarRole(string id, PerfilUserEnum novoPerfil)
        {
            var result = await _usuarioService.AlterarRoleAsync(id, novoPerfil);

            if (!result.Succeeded)
            {
                TempData["Erro"] = "Não foi possível alterar o perfil do usuario";
                return RedirectToAction(nameof(Index));
            }

            TempData["Sucesso"] = "Perfil do usuario alterado com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AlterarSenha(string id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            
            if(usuario == null)
            {
                return NotFound();
            }

            var model = new UsuarioResetPasswordViewModel
            {
                Id = usuario.Id,
                Senha = string.Empty,
                ConfirmarSenha = string.Empty
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarSenha(UsuarioResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _usuarioService.ResetPasswordAsync(
                model.Id,
                model.Senha
            );

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        } 
    }
}