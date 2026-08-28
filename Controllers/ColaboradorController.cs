using Controle_de_Epis.Service.Interfaces;
using Controle_de_Epis.ViewModel.Colaborador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Epis.Controllers
{
    [Authorize(Roles = "Admin,Opeerador")]
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
            var colaborador = await _icolaboradorservice.GetAllColaboradoresAsync);
            return View(colaborador);
        }

        [HttpGet]
        public IActionResult CriarColaborador()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarColaborador(colaborador)
        {
            if (!ModelState.IsValid) 
            {
                return View(colaborador);
            }

            var resultado = await _icolaboradorservice.CriarColaboradorAsync(colaborador);

            if (resultado.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach(var erro in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Decription);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarColaborador(EditarColaboradorViewModel colaborador)
        {
            var colaborador = await _icolaboradorservice.GetColaboradorByIdAsync(id);

            return View(colaborador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarColaorador(EditarColaboradorViewModel colaborador) 
        {
            if (!ModelState.IsValid)
            {
                return Redirect(nameof(Index));
            }

            var resultado = await _icolaboradorservice.UpdateColaboradorAsync(colaborador);

            if (resultado.Sucesso)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach(var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Descripcion);
            }

            return View(colaborador);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarStatus(bool alterarStatus)
        {
            var resultado = await _icolaboradorservice.AlterarStatusAsync(alterarStatus);

            if (!resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }



    }
}
