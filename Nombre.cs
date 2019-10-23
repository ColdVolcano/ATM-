using ATMPlus.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATMPlus
{
    public struct Nombre
    {
        public Nombre(Cuenta acc)
        {
            PrimerNombre = acc.PrimerNombre;
            PrimerApellido = acc.PrimerApellido;
            SegundoApellido = acc.SegundoApellido;
            SegundoNombre = acc.SegundoNombre;
        }

        public readonly string PrimerNombre;
        public readonly string PrimerApellido;
        public readonly string SegundoApellido;
        public readonly string SegundoNombre;
        public string NombreCompleto => PrimerNombre + " " + (string.IsNullOrEmpty(SegundoNombre) ? "" : SegundoNombre + " ") + PrimerApellido + " " + SegundoApellido;
    }
}
