using System;
using System.Collections.Generic;
using System.Text;
using ATMPlus.Database;

namespace ATMPlus
{
    public interface ICuenta
    {
        int NumeroCuenta { get; set; }
        Nombre Nombre { get; set; }
    }
}
