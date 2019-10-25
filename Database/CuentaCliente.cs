using ATMPlus.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMPlus.Database
{
    public class CuentaCliente : ICuenta
    {
        [Key]
        [Column("NumeroCuenta", TypeName = "int")]
        public int NumeroCuenta { get; set; }

        [Required]
        [Column("Saldo", TypeName = "float")]
        public double Saldo { get; set; }

        [NotMapped]
        public Nombre Nombre { get; set; }

        [NotMapped]
        public readonly List<PendingDeposit> DepositosPendientes = new List<PendingDeposit>();

    }
}
