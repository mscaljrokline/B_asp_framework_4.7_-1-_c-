using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OPERACION_AMHCH_5_OK.Models
{
    public class Cuenta2
    {

        public string NRO_CUENTA { get; set; }
        public string NRO_CUENTA2 { get; set; }
        public DateTime FECHA { get; set; }
        public string TIPO { get; set; }
        public string MONEDA { get; set; }
        public string NOMBRE { get; set; }
        public double SALDO { get; set; }

    }
}