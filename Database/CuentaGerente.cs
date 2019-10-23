using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ATMPlus.Database
{
    public class CuentaGerente : ICuenta
    {
        [Key]
        [Column("NumeroCuenta", TypeName = "int")]
        public int NumeroCuenta { get; set; }

        public int NumeroEmpleado { get; set; }

        [NotMapped]
        public Nombre Nombre { get; set; }
    }
}
