using OPERACION_AMHCH_5_OK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OPERACION_AMHCH_5_OK.Controllers
{
    public class OperacionController : Controller
    {
        // GET: Operacion
        public ActionResult Index()
        {
            ViewBag.cuentas = DbCuenta.listar_cuenta();
            return View();
        }
        [HttpPost]
        public ActionResult Operar(string accion)
        {
            decimal valorDecimal;
            string CUENTA = Request.Form["CUENTA"];
            string MONTO = Request.Form["MONTO"];


            string aa = "";
            if (decimal.TryParse(MONTO, out valorDecimal))
            {
                if (accion=="deposito")
                {
                    if (DbCuenta.Operacion(CUENTA, valorDecimal,"pro_deposito_cuenta"))
                    {
                        aa = "Deposito correcto";
                    }
                    else
                    {
                        aa = "Deposito no valido";
                    }
                }
                else
                {
                    if (DbCuenta.Operacion(CUENTA, valorDecimal, "pro_retiro_cuenta"))
                    {
                        aa = "Retiro correcto";
                    }
                    else
                    {
                        aa = "No se dispone del monto o fallo";
                    }
                }
            }
            else
            {
                ViewBag.errormonto = "Monto no valido";
            }
            ViewBag.error = aa;
            ViewBag.monto = MONTO;
            ViewBag.cuentas = DbCuenta.listar_cuenta();
            return View("Index");
        }
    }
}