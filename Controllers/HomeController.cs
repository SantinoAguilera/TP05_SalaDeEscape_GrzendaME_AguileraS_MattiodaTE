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
        if (Escape.GetEstadoJuego() < 11)
        {
            ViewBag.error = true;
            ViewBag.estadoSalaID = Escape.RevisarEstadoSala(21);
            return View("habitaciones/habitacion2/habitacion21");
        }
        else return View();
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
            if (Escape.GetEstadoJuego() == 5 && contrasena.ToLower() == Escape.incognitasSalas[2]) Escape.AvanzarEstado(); //Lo avanzo de nuevo para saltear la respuesta incorrecta
        }
        else
        {
            ViewBag.error = true;
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
            ViewBag.error = true;
        }
        return View("habitaciones/habitacion" + url);
    }

    public IActionResult BotonesStanca(int botonNum)
    {
        if (Escape.ResolverStanca(botonNum)) Escape.AvanzarEstado();
        switch(Escape.CheckearStanca())
        {
            case 0:
                ViewBag.hechoStanca = "ERROR";
                break;
            case 1:
                ViewBag.hechoStanca = "CORREGIDO";
                break;
            case 2:
                ViewBag.hechoStanca = "ENVIADO";
                break;
            case 3:
                ViewBag.hechoStanca = "how, enviar no se puede tocar antes de corregir";
                break;
        }
        return View("habitaciones/habitacion4/habitacion43");
    }

    public IActionResult BotonesCaja(string num)
    {
        ViewBag.codigoCaja = Escape.ResolverCaja(num);
        ViewBag.estadoSalaID = Escape.RevisarEstadoSala(52);
        return View("habitaciones/habitacion5/habitacion52");
    }

    public IActionResult CajaEnter()
    {
        if (Escape.CheckearCaja()) Escape.AvanzarEstado();
        else ViewBag.error = true;
        ViewBag.estadoSalaID = Escape.RevisarEstadoSala(52);
        return View("habitaciones/habitacion5/habitacion52");
    }

    public IActionResult CajaBackspace()
    {
        ViewBag.codigoCaja = Escape.BackspaceCaja();
        ViewBag.estadoSalaID = Escape.RevisarEstadoSala(52);
        return View("habitaciones/habitacion5/habitacion52");
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
        if (estadoDialogo == 0) Escape.AvanzarEstado();

        ViewBag.estadoDialogo = estadoDialogo;

        return View("habitaciones/dialogo" + dialogo);
    }

    public IActionResult Llaves(int sala)
    {
        Escape.AvanzarEstado();
        ViewBag.estadoSalaID = Escape.RevisarEstadoSala(52);
        return View("habitaciones/habitacion5/habitacion5" + sala);
    }

    /*public IActionResult PistasAbrir()
    {
        
    }*/

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
