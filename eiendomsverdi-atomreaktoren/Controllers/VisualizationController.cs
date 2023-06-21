using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eiendomsverdi_atomreaktoren.Models;
using eiendomsverdi_atomreaktoren.Interfaces;
using eiendomsverdi_atomreaktoren.Db;
using eiendomsverdi_atomreaktoren.Services;

namespace eiendomsverdi_atomreaktoren.Controllers;

public class VisualizationController : Controller
{
    private readonly ILogger<VisualizationController> _logger;
    private readonly IReactorControlUnit _reactorControlUnit;

    public VisualizationController(ILogger<VisualizationController> logger, IReactorControlUnit reactorControlUnit)
    {
        _logger = logger;
        _reactorControlUnit = reactorControlUnit;
    }

    public IActionResult Index()
    {
        return View(_reactorControlUnit.GetPressureHistory().ToList());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

