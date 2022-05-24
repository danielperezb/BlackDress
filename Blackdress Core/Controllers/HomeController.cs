using Blackdress_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Blackdress_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult vestidos()
        {
            return View("Views/Home/Vestidos.cshtml");
        }
        public IActionResult darksideprincessvintageclasiclolitaop()
        {
            return View("Views/Home/Lolitakei/" +
                "Thedarksideofprincessvintageclassiclolitaop.cshtml");
        }
        public IActionResult Suzuyajacket()
        {
            return View("Views/Home/Lolitakei/Suzuyajacket.cshtml");
        }
        public IActionResult themoontights()
        {
            return View("Views/Home/Lolitakei/Themoontights.cshtml");
        }
        public IActionResult sailoracademy()
        {
            return View("Views/Home/Lolitakei/JuniorSailorAcademy.cshtml");
        }
        public IActionResult thesacredcollege()
        {
            return View("Views/Home/Lolitakei/Thesacredcollege.cshtml");
        }
        public IActionResult jiuweihusailorlolita()
        {
            return View("Views/Home/Lolitakei/jiuweihusailorlolita.cshtml");
        }
        public IActionResult abrigos()
        {
            return View("Views/Home/Abrigos.cshtml");
        }
        public IActionResult sweetmilkcoat()
        {
            return View("Views/Home/Lolitakei/sweetmilkcandygirlcoat.cshtml");
        }
        public IActionResult crownedredcoat()
        {
            return View("Views/Home/Lolitakei/crownedcoatreturnofthered.cshtml");
        }
        public IActionResult theravencoat()
        {
            return View("Views/Home/Lolitakei/Theravencoat.cshtml");
        }
        public IActionResult blusas()
        {
            return View("Views/Home/Blusas.cshtml");
        }
        public IActionResult constelacionblusa()
        {
            return View("Views/Home/Lolitakei/constelacionclasiclolita.cshtml");
        }
        public IActionResult vintageblusa()
        {
            return View("Views/Home/Lolitakei/Vintageclassiclolitablouse.cshtml");
        }
        public IActionResult pumkinblusa()
        {
            return View("Views/Home/Lolitakei/pumkincatblouse.cshtml");
        }
        public IActionResult zapatos()
        {
            return View("Views/Home/Zapatos.cshtml");
        }
        public IActionResult accesorios()
        {
            return View("Views/Home/accesorios.cshtml");
        }
        public IActionResult sweetlolitashoes()
        {
            return View("Views/Home/Lolitakei/sweetbowslolitashoes.cshtml");
        }
        public IActionResult highplatformskyblue()
        {
            return View("Views/Home/Lolitakei/Highplatforskyblue.cshtml");
        }
        public IActionResult cataccesorio()
        {
            return View("Views/Home/Lolitakei/cataccesorio.cshtml");
        }
        public IActionResult cureyoursoul()
        {
            return View("Views/Home/Lolitakei/cureyoursoul.cshtml");
        }
        public IActionResult petticoatbell()
        {
            return View("Views/Home/Lolitakei/petticoatbell.cshtml");
        }
        public IActionResult boletitacliente()
        {
            return View("Views/Home/BoletaCliente.cshtml");
        }
        public IActionResult carritodecompra()
        {
            return View("Views/Home/carritodecompra.cshtml");
        }
        public IActionResult genpedprov()
        {
            return View("Views/Home/Pedidosproveedores.cshtml");
        }
        public IActionResult bodega()
        {
            return View("Views/Home/Bodega.cshtml");
        }
        public IActionResult registrarse()
        {
            return View("Views/Home/registrarse.cshtml");
        }
        public IActionResult Estadoconfecciones()
        {
            return View("Views/Home/Estadoconfecciones.cshtml");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
