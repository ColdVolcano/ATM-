using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ATMPlus.Database
{
    public class HistorialRetiro : IHistorial
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CuentaOrigen { get; set; }

        [Required]
        [Column("FechaHora", TypeName = "datetime")]
        public DateTime FechaHora { get; set; }

        [Required]
        [Column("Cantidad", TypeName = "float")]
        public double Cantidad { get; set; }

        public override string ToString()
        {
            return FechaHora.ToString("MM/yyyy");
        }
    }
}
