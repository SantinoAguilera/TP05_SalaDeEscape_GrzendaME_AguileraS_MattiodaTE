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

    public IActionResult Comenzar()
    {
        Escape.InicializarJuego();
        return View("habitacion1");
    }

    public IActionResult Habitacion(int sala)
    {
        return View("habitacion" + sala); //Va a la sala indicada por el parámetro
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
