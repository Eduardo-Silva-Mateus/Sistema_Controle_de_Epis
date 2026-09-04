using Controle_de_Epis.Results;
using Controle_de_Epis.Service;
using Controle_de_Epis.Service.Interfaces;
using Controle_de_Epis.ViewModel.Colaborador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Epis.Controllers
{
    [Authorize(Roles = "Admin,Operador")]
    public class ColaboradorController : Controller
    {
        private readonly IColaboradorService _icolaboradorservice;
        public ColaboradorController(IColaboradorService colaboradorserice)
        {
            _icolaboradorservice = colaboradorserice;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var colaboradores = await _icolaboradorservice
                .GetAllColaboradoresAsync();

            return View(colaboradores);
        }

        [HttpGet]
        public IActionResult CriarColaborador()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarColaborador(CriarColaboradorViewModel colaborador)
        {
            if (!ModelState.IsValid)
            {
                return View(colaborador);
            }

            var resultado = await _icolaboradorservice
                .CriarColaboradorAsync(colaborador);

            if (resultado.Sucesso)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                resultado.Erro ?? "Não foi possível cadastrar o colaborador.");

            return View(colaborador);
        }

        [HttpGet]
        public async Task<IActionResult> EditarColaborador(int id)
        {
            var resultado = await _icolaboradorservice
                .GetColaboradorByIdAsync(id);

            if (!resultado.Sucesso)
            {
                TempData["Erro"] = resultado.Erro;
                return RedirectToAction(nameof(Index));
            }

            return View(resultado.Dados);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarColaborador(
            EditarColaboradorViewModel colaborador)
        {
            if (!ModelState.IsValid)
            {
                return View(colaborador);
            }

            var resultado = await _icolaboradorservice
                .UpdateColaboradorAsync(colaborador);

            if (resultado.Sucesso)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                resultado.Erro ?? "Não foi possível atualizar o colaborador.");

            return View(colaborador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarStatus(int id)
        {
            var resultado = await _icolaboradorservice
                .AlterarStatusAsync(id);

            if (!resultado.Sucesso)
            {
                TempData["Erro"] = resultado.Erro;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
