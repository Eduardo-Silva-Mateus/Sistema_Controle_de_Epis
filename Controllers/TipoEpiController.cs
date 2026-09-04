using Controle_de_Epis.Service.Interfaces;
using Controle_de_Epis.ViewModel.TipoEpi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Epis.Controllers
{
    [Authorize(Roles = "Admin,Operador")]
    public class TipoEpiController : Controller
    {
        private readonly ITipoEpiService _tipoEpiService;
        public TipoEpiController(ITipoEpiService tipoEpiService)
        {
            _tipoEpiService = tipoEpiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tipoEpis = await _tipoEpiService
                .ListarTipoEpisAsync();

            return View(tipoEpis);
        }

        [HttpGet]
        public IActionResult CriarTipoEpi()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken ]
        public async Task<IActionResult> CriarTipoEpi(CriarTipoEpiViewModel tipoEpi)
        {
            if (!ModelState.IsValid)
            {
                return View(tipoEpi);
            }
            var resultado = await _tipoEpiService.CriarTipoEpiAsync(tipoEpi);

            if (resultado.Sucesso)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                resultado.Erro ?? "Não foi possível cadastrar o tipo de EPI.");

            return View(tipoEpi);
        }

        [HttpGet]
        public async Task<IActionResult> AlterarStatusTipoEpi(int id, bool status)
        {
            var resultado = await _tipoEpiService.AlterarStatusTipoEpiAsync(id, status);

            if (resultado.Sucesso)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                resultado.Erro ?? "Não foi possível alterar o status do tipo de EPI.");

            return RedirectToAction(nameof(Index));
        }

    }
}
