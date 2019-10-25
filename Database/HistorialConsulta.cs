using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMPlus.Database
{
    public class HistorialConsulta : IHistorial
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int CuentaOrigen { get; set; }

        [Required]
        [Column("FechaHora", TypeName = "datetime")]
        public DateTime FechaHora { get; set; }

        public override string ToString()
        {
            return FechaHora.ToString("MM/yyyy");
        }
    }
}
