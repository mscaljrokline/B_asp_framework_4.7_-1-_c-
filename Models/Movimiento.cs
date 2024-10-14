using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OPERACION_AMHCH_5_OK.Models
{
    public class Movimiento
    {


        public DateTime FECHA { get; set; }
        public double IMPORTE { get; set; }
        public string TIPO { get; set; }

    }
}