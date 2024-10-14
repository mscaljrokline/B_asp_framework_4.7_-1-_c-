using OPERACION_AMHCH_5_OK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OPERACION_AMHCH_5_OK.Controllers
{
    public class SaldoController : Controller
    {
        // GET: Saldo
        public ActionResult Index()
        {
            ViewBag.cuentas = DbCuenta.listar_cuenta();
            return View();
        }
    }
}