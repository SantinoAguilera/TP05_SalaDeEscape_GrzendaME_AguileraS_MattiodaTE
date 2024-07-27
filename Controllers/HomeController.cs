using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP05_SalaDeEscape_GrzendaME_AguileraS_MattiodaTE.Models;

namespace TP05_SalaDeEscape_GrzendaME_AguileraS_MattiodaTE.Controllers;

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

    public IActionResult Tutorial()
    {
        return View();
    }

    public IActionResult Creditos()
    {
        return View();
    }

    public IActionResult Victoria()
    {
        return View();
    }

    public IActionResult Comenzar()
    {
        Escape.InicializarJuego();
        ViewBag.estadoSalaID = 1;
        return View("habitaciones/habitacion1/habitacion11");
    }

    public IActionResult Habitacion(int sala, int salaAnterior = 0, int estadoMin = 0, int contrasenaAceptada = -1)
    {
        string url;
        if (Escape.GetEstadoJuego() >= estadoMin)
        {
            ViewBag.estadoSalaID = Escape.RevisarEstadoSala(sala);
            url = "habitacion" + sala.ToString().Substring(0, 1) + "/habitacion" + sala;
        }
        else
        {
            ViewBag.estadoSalaID = Escape.RevisarEstadoSala(salaAnterior);
            ViewBag.salaID = sala;
            ViewBag.salaAnteriorID = salaAnterior;
            ViewBag.contrasenaAceptada = contrasenaAceptada;
            url = "contrasena";
        }
        return View("habitaciones/" + url);
    }

    public IActionResult Resolver(int sala, string contrasena)
    {
        if (Escape.ResolverSala(contrasena, -1)) Escape.AvanzarEstado();
        ViewBag.estadoSalaID = Escape.RevisarEstadoSala(sala);
        return View("habitaciones/habitacion" + sala.ToString().Substring(0, 1) + "/habitacion" + sala);
    }

    public IActionResult ResolverSala(int sala, int salaAnterior, int contrasenaAceptada, string contrasena)
    {
        string url;
        if (Escape.ResolverSala(contrasena, contrasenaAceptada))
        {
            Escape.AvanzarEstado();
            ViewBag.estadoSalaID = Escape.RevisarEstadoSala(sala);
            url = sala.ToString().Substring(0, 1) + "/habitacion" + sala;
        }
        else
        {
            ViewBag.estadoSalaID = Escape.RevisarEstadoSala(salaAnterior);
            url = salaAnterior.ToString().Substring(0, 1) + "/habitacion" + salaAnterior;
        }
        return View("habitaciones/habitacion" + url);
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
