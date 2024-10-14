using OPERACION_AMHCH_5_OK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OPERACION_AMHCH_5_OK.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult GuardarCuenta(Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                cuenta.SALDO = 0.00;
                cuenta.TIPO = (cuenta.NRO_CUENTA.Length == 13) ? "CTE" : "AHO";
                string val = DbCuenta.Guardar(cuenta);
                if (val == "")
                {
                    ViewBag.men = "Guardado exitoso";
                }
                else
                {
                    ViewBag.men = val;
                }
            }
            return View("Index", cuenta);
        }
    }
}