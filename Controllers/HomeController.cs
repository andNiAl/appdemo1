using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using appdemo.Models;

namespace appdemo.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }

     [HttpGet]
    public IActionResult Operar()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpPost]
    public ActionResult Operar(OperacionBolsa operacion)
    {
        if (ModelState.IsValid)
    {
        // Lógica para procesar la operación
        double total = 0;
        double IGV = 0;
        double resultadoEjercicio = 0;
        double IGVSP500=0,IGVDowJones=0,IGVBonosUS=0;

        if (operacion.SP500)
        {
            IGVSP500 = 500*0.18;
            total += 500 + IGVSP500;
        }
        if (operacion.DowJones)
        {
            IGVDowJones = 300*0.18;
            total += 300 + IGVDowJones;
        }
        if (operacion.BonosUS)
        {
            IGVBonosUS = 120*0.18;
            total += 120 + IGVBonosUS;
        }

        // Aplicar la comisión basada en el monto a abonar
        double comision = operacion.MontoAbonar > 300 ? 1 : 3;
        resultadoEjercicio = total + (double)operacion.MontoAbonar;
        resultadoEjercicio += comision;

        //Mostrar resultado

        if(total != 0){

            ViewBag.DatosOperacion = operacion;

            ViewBag.TotalTabla = total;

            ViewBag.TotalFinal = resultadoEjercicio;

            ViewBag.Comision = comision;

            // mostrar todos los IGV

            ViewBag.IGVSP500 = IGVSP500;
            ViewBag.IGVDowJones = IGVDowJones;
            ViewBag.IGVBonosUS = IGVBonosUS;
        }

        return View("Operar");
    }

    return View(operacion);
    }


    public class CalculadoraBolsa
    {
        public static decimal CalcularIGV(decimal monto)
        {
            return monto * 0.18m;
        }

    }
}
