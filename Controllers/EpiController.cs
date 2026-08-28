using Controle_de_Epis.Infrastructure.Identity;
using Controle_de_Epis.AspNetCore.Mvc;
using System.Diagnostics;

namespace Controle_de_Epis.Controllers
{
    public class EpiController : Controller
    {
        public IActionResult Index()
        {
            return Index();
        }
    }
}