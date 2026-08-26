using Controle_de_Epis.Service.Interfaces;
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
        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]





    }
}
