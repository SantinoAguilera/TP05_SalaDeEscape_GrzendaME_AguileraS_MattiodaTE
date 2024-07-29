using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
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
        if (Escape.ResolverSala(contrasena, -1))
        {
            Escape.AvanzarEstado();
            if (Escape.GetEstadoJuego() == 5 && contrasena == Escape.incognitasSalas[2]) Escape.AvanzarEstado(); //Lo avanzo de nuevo para saltear la respuesta incorrecta
        }
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

    public IActionResult BotonesStanca(int num, int botonNum)
    {
        bool resultado = Escape.ResolverStanca(num, botonNum);
        if (resultado)
        {
            Escape.AvanzarEstado();
        }
        return View("habitaciones/habitacion4/habitacion43");
    }

    public IActionResult Marcador(int sala, string marcador)
    {
        ViewBag.salaID = sala;
        ViewBag.estadoSalaID = Escape.RevisarEstadoSala(sala);
        ViewBag.marcador = marcador;

        return View("habitaciones/marcador");
    }

    public IActionResult Dialogo(int dialogo, int estadoDialogo = 0)
    {
        Escape.AvanzarEstado();

        ViewBag.estadoDialogo = estadoDialogo;

        return View("habitaciones/dialogo" + dialogo);
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
