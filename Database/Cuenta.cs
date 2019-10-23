using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMPlus.Database
{
    public class Cuenta
    {
        [Key]
        public int NumeroCuenta { get; set; }

        [Required]
        [Column("PrimerNom", TypeName = "varchar(50)")]
        public string PrimerNombre { get; set; }

        [Required]
        [Column("PrimerApe", TypeName = "varchar(50)")]
        public string PrimerApellido { get; set; }

        [Required]
        [Column("SegundoApe", TypeName = "varchar(50)")]
        public string SegundoApellido { get; set; }

        [Column("SegundoNom", TypeName = "varchar(50)")]
        public string SegundoNombre { get; set; }

        [Required]
        [Column("PinPass", TypeName = "char(40)")]
        public string PinPass { get; set; }

        [Required]
        [Column("TipoCuenta", TypeName = "tinyint")]
        public TipoCuenta TipoCuenta { get; set; }
    }
}
