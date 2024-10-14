using OPERACION_AMHCH_5_OK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OPERACION_AMHCH_5_OK.Controllers
{
    public class TransferenciaController : Controller
    {
        // GET: Transferencia
        public ActionResult Index()
        {
            ViewBag.cuentas = DbCuenta.listar_cuenta();
            return View();
        }
        [HttpPost]
        public ActionResult Transferir(string accion)
        {
            decimal valorDecimal;
            string ORIGEN = Request.Form["ORIGEN"];
            string DESTINO = Request.Form["DESTINO"];
            string MONTO = Request.Form["MONTO"];


            string aa = "";
            if (decimal.TryParse(MONTO, out valorDecimal))
            {
                if (DbCuenta.Operacion(ORIGEN, valorDecimal, "pro_retiro_cuenta"))
                {
                    if (DbCuenta.Operacion(DESTINO, valorDecimal, "pro_deposito_cuenta"))
                    {
                        aa = "Transferencia correcta";
                    }
                    else
                    {
                        aa = "Deposito no valido";
                    }
                }
                else
                {
                    aa = "No dispone del monto";
                }
            }
            else
            {
                ViewBag.errormonto = "Monto no valido";
            }
            ViewBag.error = aa;
            ViewBag.cuentas = DbCuenta.listar_cuenta();
            return View("Index");
        }
        [HttpPost]
        public ContentResult Saldo(string ORIGEN)
        {
            // Procesar el valor recibido
            string valorRecibido = ORIGEN;
            string ret = DbCuenta.obtener_saldo(valorRecibido);
            return Content(ret);

        }
    /*
        [HttpPost]
        public JsonResult TuAccion(string ORIGEN)
        {
            // Procesar el valor recibido
            string valorRecibido = ORIGEN;

            // Devolver una respuesta JSON opcionalmente
            return Json(new { resultado = "Valor recibido: " + valorRecibido });
        }
    */
    }
}