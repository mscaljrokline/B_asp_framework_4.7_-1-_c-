using OPERACION_AMHCH_5_OK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OPERACION_AMHCH_5_OK.Controllers
{
    public class MovimientoController : Controller
    {
        // GET: Movimiento
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult Recibe(string NRO_CUENTA, string NRO_CUENTA2, string MONEDA, string NOMBRE, string SALDO)
        {
            ViewBag.cuenta = NRO_CUENTA2;
            ViewBag.moneda = MONEDA;
            ViewBag.NOMBRE = NOMBRE;
            ViewBag.saldo = SALDO;
            ViewBag.movs = DbCuenta.listar_mov(NRO_CUENTA);

            return View("Index");
        }
    }
}