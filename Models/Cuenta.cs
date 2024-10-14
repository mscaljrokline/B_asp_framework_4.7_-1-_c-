using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OPERACION_AMHCH_5_OK.Models
{
    public class Cuenta
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(14, MinimumLength =13, ErrorMessage ="Debe ser 13 o 14 caracteres")]
        public string NRO_CUENTA { get; set; }
        public DateTime FECHA { get; set; }
        public string TIPO { get; set; }
        public string MONEDA { get; set; }
        [Required(ErrorMessage ="Nombre obligatorio")]
        [StringLength(40, ErrorMessage ="Pueden ser maximo 40 letras")]
        public string NOMBRE { get; set; }
        public double SALDO { get; set; }

    }
}